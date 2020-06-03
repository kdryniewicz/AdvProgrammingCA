using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CA_Project
{
    /// <summary>
    /// Konrad Dryniewicz - S00160273
    /// For most part the application works, the isolated storage methods are implemented but do not work fully I believe...
    /// </summary>
    public partial class MainWindow : Window
    {
        [ThreadStaticAttribute()]
        private static int CurrentGold = 0; //Value to store player's gold or score

        //Lock used to keep track of when each thread tries to access something
        private static object MinerLock = new object();

        //Amount of gold mined per click (through button)
        public int goldMineAmt = 1;
        //Rate at which clicking earns gold
        private float btnMineRate = 1.0f;
        //Amount of gold earned by each miner (e.g. miner A will earn this amount x rate he has set)
        private int MinerEarnRate = 2; 
        //list to keep track of all miners
        private List<Miner> Miners = new List<Miner>();
        //Array to keep track of threads in appliaction
        private Thread[] threads = new Thread[3];

        //A background worker used to iterate and update UI elements
        public BackgroundWorker GameWorker = new BackgroundWorker();

        //A bool used to determine if the miners started working or not
        private bool minersActive = false;
        //Used to keep track of how much it costs to buy miners.
        int MinersCost = 10;
        //Used to check if miners were purchased or not
        private bool MinersPurchased = false;
        //Used when setting up the threads and bg worker
        private bool MinersSetUp = false;
        //Rates (beside progress bars) to keep track of how much each miner earned during the run.
        List<Label> txtRates = new List<Label>();


        //Isolated Storage

        //isolated store - folder - will hold our own named folders and isolated storage files
        private IsolatedStorageFile store;
        //create the name of a named folder
        string folderName;
        //path to an isolated storage file
        string pathToFile;
        //A list to store the logs of which miner earned what.
        private List<string> earnHistory = new List<string>();

        public MainWindow()
        {
            InitializeComponent();


            //First set up the miners
            Miners.Add(CreateMiner("MinerA", 10, 1.0f, 1));
            Miners.Add(CreateMiner("MinerB", 30, 1.5f, 1.2f));
            Miners.Add(CreateMiner("MinerC", 100, 2.0f, 1.5f));

            //Add the rates to the list for easier iteration
            txtRates.Add(txMinerAValue);
            txtRates.Add(txMinerBValue);
            txtRates.Add(txMinerCValue);


            //prepare the background worker.
            GameWorker.DoWork += Worker_DoWork;
            GameWorker.ProgressChanged += Worker_ProgressChanged;
            GameWorker.RunWorkerCompleted += Worker_Completed;
            GameWorker.WorkerSupportsCancellation = true;
            GameWorker.WorkerReportsProgress = true;

            for (int i = 0; i < txtRates.Count; i++)
            {
                txtRates[i].Content = "+" + (int)(MinerEarnRate * Miners[i].mineRate);
            }

            //Prepare the text for buying miner in the shop
            txtBuyMiner.Text = string.Format(txtBuyMiner.Text + "Costs: {0}", MinersCost);

            txtGold.Content = CurrentGold;

            //Isolated storage set up.
            store = IsolatedStorageFile.GetUserStoreForDomain();
            folderName = "Earnings";
            pathToFile = String.Format("{0}\\MinerHistory.txt", folderName);

            threads[1] = new Thread(new ParameterizedThreadStart(ThreadWriter));
            threads[1].Name = "History Writer";
            threads[1].Priority = ThreadPriority.AboveNormal;

            threads[2] = new Thread(new ThreadStart(ThreadReader));
            threads[2].Name = "History Reader";
            threads[2].Priority = ThreadPriority.Normal;

        }

        
        //Below are two methods of writing and reading from storage.
        void ThreadWriter(object obj)
        {
            try
            {
                string line = (string)obj;
                AccessStorage(line, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ThreadReader()
        {
            try
            {
                threads[2].Join();
                AccessStorage(string.Empty, false);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuyMiner_Click(object sender, RoutedEventArgs e)
        {
            //First we check if we still need to unlock any miners.
            if (Miners.Count > 0)
            {
                if (!minersActive)
                {
                    if (CurrentGold >= MinersCost)
                    {
                        MessageBoxResult res = MessageBox.Show(string.Format("Are you sure you want to purchase the Miners for {0}?", MinersCost), "Confirmation", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                        {
                            CurrentGold -= MinersCost;
                            MinersPurchased = true;

                            MessageBox.Show(string.Format("Miners have been purchased!"));
                            btnBuyMiner.IsEnabled = false;


                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        MessageBox.Show("Not Enough Gold!");
                    }
                }
                else
                {
                    MessageBox.Show("You already purchased the miners.");
                }

            }
            else
            {
                //We can disable the button if we can't buy any more miners (since only 3 can be unlocked.
                btnBuyMiner.IsEnabled = false;
                MessageBox.Show("All Miners have been already purchased!");
            }
            txtGold.Content = CurrentGold;

        }

        

        //Creates the three miners with different rates and names
        private Miner CreateMiner(string Name, int cost, float MineSpeed, float MineRate)
        {
            Miner temp = new Miner();
            if (Name.Contains("A"))
            {
                temp = new Miner()
                {
                    name = Name,
                    cost = cost,
                    mineRate = MineRate,
                    bar = barMinerA,
                    mineSpeed = MineSpeed,
                };
            }
            else if (Name.Contains("B"))
            {
                temp = new Miner()
                {
                    name = Name,
                    cost = cost,
                    mineRate = MineRate,
                    bar = barMinerB,
                    mineSpeed = MineSpeed,
                };
            }
            else if (Name.Contains("C"))
            {
                temp = new Miner()
                {
                    name = Name,
                    cost = cost,
                    mineRate = MineRate,
                    bar = barMinerC,
                    mineSpeed = MineSpeed,
                };
            }
            
            temp.bar.Maximum = 100;
            //Value and maxvalue used in thread working then passed to bg worker for updating
            temp.valueMax = (int)temp.bar.Maximum;
            temp.value = 0;
            return temp;
        }

        //When earning gold manually, every click add gold.
        private void btnMineResources_Click(object sender, RoutedEventArgs e)
        {

            CurrentGold += (int)(goldMineAmt * btnMineRate);
            txtGold.Content = CurrentGold;

        }
        

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //First check if the miners got activated
            while (minersActive)
            {
                foreach (Miner m in Miners)
                {
                    //Dispatcher Invoke to allow interaction with UI thread from BG Worker
                    Dispatcher.Invoke(new Action(delegate {
                        m.bar.Value = m.value;
                        m.bar.Maximum = m.valueMax;
                        m.bar.ToolTip = string.Format("{0} / {1} completed...", m.value, m.valueMax);

                        if (m.value >= m.valueMax) //if our bar is full - reset it back to zero
                        {
                            m.bar.Value = 0;
                            //lbxLogs.Items.Add(string.Format("{0} has earned {1} gold!", m.name, m.earnedGold));
                        }
                    }));
                    if(m.value >= m.valueMax)
                    {
                        PassInfoToThreads(m.ToString());
                    }
                }
                //Update the rates
                for (int i = 0; i < txtRates.Count; i++)
                {
                    Dispatcher.Invoke(new Action(delegate {
                        txtRates[i].Content = "+" + Miners[i].earnedGold;
                    }));
                }
                //update current gold
                Dispatcher.Invoke(new Action(delegate {
                    txtGold.Content = CurrentGold;
                    lbxLogs.ItemsSource = earnHistory;

                }));

            }
        }
   
        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private int PassGold(object item)
        {
            int convertedGold = (int)item;
            return convertedGold;
        }

        //Method for Thread 0, to allow our miners to earn gold
        private void IncrementerGold()
        {
            //First we check if the miners were activated
            while (minersActive)
            {
  
                    foreach (Miner current in Miners)
                    {
                    //For every miner, while the progressbar is not full, 
                    //keep adding to it and go to sleep (to simulate some work being done and make sure bar doesn't fill up instantly.)
                        for (int i = 0; i < current.valueMax; i++)
                        {
                            if (current.value < current.valueMax)
                            {
                                current.value += (int)(2 * current.mineSpeed);
                                break;
                            } 
                        }
                        //If our bar is full, add some gold (per each miner) and reset itself back again.
                    if (current.value >= current.valueMax)
                    {
                        current.earnedGold = (int)(MinerEarnRate * current.mineRate);
                        CurrentGold += current.earnedGold;
                        
                        Dispatcher.Invoke(new Action(delegate {
                            txtGold.Content = CurrentGold;
                        }));
                        current.value = 0;


                    }
                }
                    //pause the thread for 100milliseconds to simulate some "work being done".
                    Thread.Sleep(100);

            }
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void barMinerA_ProgressBarC()
        {

        }

        private void barMinerA_Value()
        {

        }

        private void barMinerA_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }



        //A method which stops the game (in case of player exit or stopping the miners manually). 
        void TerminateGame()
        {
            foreach (Thread t in threads)
            {
                if (t != null)
                {
                    if (t.ThreadState == ThreadState.Running)
                    {
                        t.Abort();
                    }
                }
            }
            GameWorker.CancelAsync();
            minersActive = false;
            MinersSetUp = false;

        }

        //The method that starts our miners to work away on earning gold
        private void btnStartMiners_Click(object sender, RoutedEventArgs e)
        {
            //Check if the miners were purchased and then prepare a thread for working.
            if (MinersPurchased)
            {
                if (!MinersSetUp)
                {
                    //threads[i] = new Thread(() => IncrementerGold(Miners[i]));
                    threads[0] = new Thread(new ThreadStart(IncrementerGold)); //We first prepare a thread for work
                    threads[0].Name = "Mining Thread";
                    threads[0].IsBackground = true; //We don't want our applcation to hang while it's working
                    Miners[0].isActive = true;

                    //These start our bg worker and thread to actually start doing stuff.
                    GameWorker.RunWorkerAsync();
                    threads[0].Start();

                    MinersSetUp = true;
                    minersActive = true;
                }
                else
                {
                    if(threads[0].ThreadState == ThreadState.Running)
                    {
                        MessageBox.Show(String.Format("Thread {0} already started", Thread.CurrentThread.Name));
                    }
                }
            }
            else
            {
                MessageBox.Show("Miners not purchased yet. You have to buy the miners to unlock them (from the shop).");
            }
        }

        //A check to make sure that if application is closed, our threads are aborted safely.
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                TerminateGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //if player wants for any reason to stop the game manually.
        private void btnStopMiners_Click(object sender, RoutedEventArgs e)
        {
            TerminateGame();
        }


        //Below are some upgrade button methods (identical to each other, with different values)
        //If player has x gold, we ask to confirm purchase and then add to specific variable accordingly.
        private void btnUpgradeClickRate_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentGold >= 20)
            {
                MessageBoxResult res = MessageBox.Show(string.Format("Are you sure you want to purchase the Upgrade for {0}?", 20), "Confirmation", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    CurrentGold -= 20;
                    btnMineRate += 1;
                    MessageBox.Show(string.Format("Upgrade has been purchased! New Rate per click is: " + btnMineRate));
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Not Enough Gold!");
            }
            txtGold.Content = CurrentGold;
        }

        private void btnUpgradeMinerSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentGold >= 35)
            {
                MessageBoxResult res = MessageBox.Show(string.Format("Are you sure you want to purchase the Upgrade for {0}?", 35), "Confirmation", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    CurrentGold -= 35;
                    foreach (Miner m in Miners)
                    {
                        m.mineSpeed += 0.1f;
                    }
                    MessageBox.Show(string.Format("Upgrade has been purchased! New Speed for each Miner is: {0}, {1} and {2} (for Miner A, B,C )", Miners[0].mineSpeed, Miners[1].mineSpeed, Miners[2].mineSpeed));
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Not Enough Gold!");
            }
            txtGold.Content = CurrentGold;
        }

        private void btnUpgradeMinerEarnRate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentGold >= 50)
            {
                MessageBoxResult res = MessageBox.Show(string.Format("Are you sure you want to purchase the Upgrade for {0}?", 50), "Confirmation", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    CurrentGold -= 50;
                    foreach (Miner m in Miners)
                    {
                        m.mineRate += 0.1f;
                    }
                    MessageBox.Show(string.Format("Upgrade has been purchased! New Speed for each Miner is: {0}, {1} and {2} (for Miner A, B,C )", Miners[0].mineRate, Miners[1].mineRate, Miners[2].mineRate));
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Not Enough Gold!");
            }
            txtGold.Content = CurrentGold;
        }

        //Method that acesses the isolated storage through writing and reading from it.
        public void AccessStorage(string s, bool Writing)
        {

            Monitor.TryEnter(MinerLock);
            try
            {
                if (store != null && Writing)
                {
                    //Check if the Directory exists, if not create it.
                    if (!store.DirectoryExists(folderName))
                        store.CreateDirectory(folderName);

                    //Create and access our Isolated storage file for history of what each miner earned.
                    using (IsolatedStorageFileStream IsoStoragefile =
                        store.OpenFile(pathToFile, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(IsoStoragefile))
                        {
                            writer.Write(s);
                        }
                    }
                }
                else
                {
                    //Now we need to access our isolated storage and read back from it.
                    using (IsolatedStorageFileStream IsoStoragefile =
                        store.OpenFile(pathToFile, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(IsoStoragefile))
                        {
                            //In order to get the input, we acces the current line, then we need to split each input by a new line.
                             string currentLines = reader.ReadToEnd();
                             earnHistory = currentLines.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

                        }

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Monitor.Pulse(MinerLock);
                Monitor.Exit(MinerLock);
            }
        }

        void PassInfoToThreads(string log)
        {
            if(!(threads[1].ThreadState == ThreadState.Running) && !(threads[1].ThreadState == ThreadState.Aborted))
            {
                threads[1].Start(string.Format("{0} written by {1}", log, threads[1].Name));
            }
            if (!(threads[2].ThreadState == ThreadState.Running))
            {
                threads[2].Start();
            }
        }

    }
}
