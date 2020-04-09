# BirthdayGreetings
My solution to the BirthdayGreetings Kata described here: http://codingdojo.org/kata/birthday-greetings/

As you’re a very friendly person, you would like to send a birthday note to all the friends you have. But you have a lot of friends and a bit lazy, it may take some times to write all the notes by hand.

The good news is that computers can do it automatically for you.

Use mocked data:
. You can just launch the app and random mocked data will be generated. This is the default situation since of course because you haven't yet executed the migration on your database so you don't have any data stored)

Use local data:
1. Execute the following commands on a terminal/package manager console:
  
    1.1 Uncomment from line 17 to 20 in BirthdayGreetingContext.cs
  
    1.2 "cd .\BirthdayGreetings.DataAccess"
  
    1.3 "dotnet ef database update"

2. Insert data in the Person table
3. Set on the "appsettings.development.json" the "UseMockedDataBase" key to false
4. Launch the application. If you have set the "UseMockedDataBase" key to false but no data was recovered from the database the app will generate mocked data

Send messages/emails:
. If you want to send email messages instead of phone messages you should just set on "appsettings.development.json" the "UseEmailService" key to true

Finally you can check on the unit tests by just displaying the Test Explorer tab and launching the tests. Before launching the tests make sure to comment from line 17 to 20 in BirthdayGreetingContext.cs
