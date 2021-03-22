# Usage
Invoke `dotnet run` from the command-line. It will beep at you non-stop when an appointment is found and show you the locations.

# Configuration
## Rite Aid
Edit RiteAid.cs and include your favorite store numbers and descriptions. Store numbers can be found on the website and you can make the descriptions whatever you ant. This program will accurately tell you where an appointment is available, when it becomes available. When you see an appointment pop up, head to the Rite Aid site and try to grab it.

### False Positives
It is important to note that just because an appointment is available, doesn't mean YOU are eligible for it. This will lead to some false positives as Rite Aid seems to prioritize certain groups at certain times (teachers, seniors, etc.). If you are not in one of those groups, you won't see an appointment when you go through the website. No personal information is submitted as part of this checker so it cannot differentiate between populations. That being said, you will always be alerted when EVERY appointment is available so if you do meet the criteria you can be certain it'll alert you and you won't miss it. 

### Annoying Alerts
There is a system beep that alerts you to availability. It is not configurable really. I turned off my sound a lot and it'll eventually clear if it is a false positive.

### Tips and Tricks
- Appointments seemed to appear most frequently between 11:00pm and 1:00am.
- You can fill out the pre-qualification on the website and let it sit on the `pick a store` page. That way when an appointment becomes available, just choose the store you were alerted to. I would advise you refresh every so often and re-fill out the pre-qualification once or twice a day.
- Rite Aid now holds your appointment for 30 minutes giving you time to fill out the form. This is a welcome update from the past when you had to race to fill things out. 
