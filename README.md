# iDLE-DETECT-v3  
PC Idle Detection Application.  
Loads XML file that contains blacklisted keywords & process names.  
Once it detects these word, it will Terminate the process.  
&nbsp;  
It will also monitor if your PC is in use or Idle mode.  
**Idle detection will occur every 5 minutes interval.**  
Once it detects that your PC is IDLE, it will prompt you to clean up the resources automatically.  
You have 1 minute to Cancel this process, click the Cancel Button anytime to cancel auto cleanup.  
&nbsp;  
**of course this app runs in the background.**  
&nbsp;
**Change Log**  
* 12/18/2017 Initial commit, added basic detection methods.  
* 3:58 PM 12/18/2017 Changes.  
&nbsp;  * Added Difinition updater (downloads & updates your list.xml)  
&nbsp;  * Added Keyword Detection using XML (list.xml)  
* 1:14 AM 12/19/2017 Changes.  
&nbsp;  * Added Close form that pops-up every 5 minutes of Idle.  
&nbsp;  &nbsp;  * Close form closes predefined processes to free-up resources.  
&nbsp;  * Added Progress bar class, this is a customizable progress bar.  
&nbsp;  * Commented a lot so that its easier to understand and track the issues later on.
&nbsp;  **Known Bug**  
&nbsp;  * After 1 minute interval when Close Form pop-up, the killing of Processes using XML is not working properly.  


