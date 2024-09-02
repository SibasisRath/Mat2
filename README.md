# Mat2 (Chest System Game)
Outscal mat 2

<p>
  <b>Currencies</b><br>
  1. Coins <br>
  2. Gems <br>
</p>


<p>
  <b>Chest Types</b><br>
  Every number here should be flexible to change by the designers and should not be hard coded! <br>
  1. Common : 100 to 200 coins and 10 to 20 gems will be gifted.  15 mins will be the unlocking time duration. <br>
  2. Rare : 100 to 200 coins and 10 to 20 gems will be gifted. 30 mins will be the unlocking time duration. <br>
  3. Epic : 100 to 200 coins and 10 to 20 gems will be gifted. 1 hour will be the unlocking time duration. <br>
  4. Legendary : 100 to 200 coins and 10 to 20 gems will be gifted. 3 hours will be the unlocking time duration. <br>
</p>

<p>
<b>Chest Slots (for storing chests)</b><br>
  1. There will be a scrollable dynamic list of slots for chests with a minimum of 4 slots where we can place chests.<br>
  2. Create a Button, which will generate random chests in the empty slot after click. (there will be different types of chests, read further to get a better idea)<br>
  3. When the chest is getting added, the timer of the chest will not start. So you need to click on the chest then one pop-up will come, and on that pop up there will be two buttons on the pop-up.<br>
    3.1. Start Timer button → Which will start the respective time for the chest depending upon the time. For starting the timer to unlock there won't be any cost.<br>
    3.2. Unlock with Gems button → Which will directly unlock the chest without the timer spending particular gems cost, for understanding more about cost read further.<br>
  4. If slots are full → Pop up saying slots are full will appear.!<br>
</p>


<p>
  <b></b>Chest Structure!</b><br>
  1. Chests can be<br>
    1.1. Locked (Timer Haven't started)<br>
    1.2. Unlocking (Timer is running on the Chest)<br>
    1.3. Unlocked but not collected (Timer is finished → Tap to collect reward state)<br>
    1.4. Collected (rewards have been collected and the chest leaves the slot that it was occupying)<br>
  2. Rewards will be received depending upon the type of the chest<br>
  3. We can start unlocking only one chest at max, which means if the timer is on for any of the chests, we can't start unlocking other chests<br>
  4. Just like the below image, we can open any unlocking chest by spending gems. The cost for that will be → 1 Gem for every 10 mins, which means suppose on the chest we have 30 mins left then cost should be 3        Gems, if 1 Hour is remaining then cost should be 6 gems, likewise. This cost of gems should also reduce as time is reduced.<br>
  5. Always take a ceil while calculating the cost , e.g. minutes are 11 ⇒ 1.1 Gem then consider the cost as 2 Gem. if minutes are 29 , then gems will be 2.9 , take cost as 3 Gems , always take a ceil.
     If gems are not enough to skip the timer then there should be one pop up saying that.<br>
  6. Implement an undo option if you want to revert your decision of spending gems for unlocking a chest through gems.<br>
</p>

<p>
  <b></b>Queueing of Chest Opening</b><br>
  1. Chests will be added in a queue to start unlocking after the current chest's timer runs down to 0 (that is unlocking)
</p>

<p>
  <b></b>Code Structure!</b><br>
  I have 4 design patterns.
  1. Dependency injection (to full fill dependency and reduce singleton)
  2. MVC (For different distinguishable entities)
  3. State pattern (different states of chests. Like Locked, queued, unlocking, unlocked)
  4. Command pattern (to impliment undo behaviour)

  I have followed SOLID and KIS principles. I have tried to write modular code with no code smell or zombie code.
</p>

Images.
![Screenshot (178)](https://github.com/user-attachments/assets/261ed2da-3990-49c5-919b-473e7e5b335b)
![Screenshot (179)](https://github.com/user-attachments/assets/7d920e03-a0a3-4d43-8256-4af4ffdd97b5)
![Screenshot (180)](https://github.com/user-attachments/assets/ef8e7de6-b659-4f31-876d-04c18ad76342)
![Screenshot (181)](https://github.com/user-attachments/assets/559b5202-aeae-4a21-a528-5decdd5a1e2b)
![Screenshot (182)](https://github.com/user-attachments/assets/58dbf302-e900-462a-aa2f-80616cd78a07)
![Screenshot (175)](https://github.com/user-attachments/assets/24bca5aa-ed49-4ec3-8af3-9d0e0a5a9581)
![Screenshot (176)](https://github.com/user-attachments/assets/cf76b068-88d1-4c2a-a27b-5dcae01c2faf)
![Screenshot (177)](https://github.com/user-attachments/assets/308a265c-4dc4-4a07-8b2b-1b91688e7063)
