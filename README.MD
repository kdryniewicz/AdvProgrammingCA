Miner Game (Clicker Game which displays the capabilites of multi threading in a WPF application)
• A game in which the main goal is to keep earning gold (either manually or automatically). 
• Auto “miners” which increases the gold / score automatically.
• A thread is used to simulate a progress of mining along with a background worker to safely invoke access to update the UI elements to display feedback.
• Another Thread is used along with Isolated Storage to write the logs of what each "Miner" has mined.
• Third thread is used to access the isolated storage again.
• A shop that would allow to use earned gold to further upgrade the manual mining or automated ones. 
• Isolated storage to keep track of logs (what each miner did and how much gold it earned). 