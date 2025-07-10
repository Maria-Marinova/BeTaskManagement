# BeTaskManagement

This is a test app.

How to run the app in VS, you will have to:
1. Download/get the code
2. Open the project in VS (I have used VS 2022)
3. Create new SQL database. You can do this in VS as well. Go to Views->Server Explorer, then connect to the SQL Server you want to use. On Data Connections right-click and Create new SQL Server Database.
4. Get the connection string for the newly created DB and paste it in "ConnectionStrings" ->"DefaultConnection".
5. Rebuild the solution.
6. In Tools->NuGet Package Manager open Package Manager Console
7. In Package Manager Console run the following command: Add-Migration NameForMigration
8. Then run Update-database
9. This should craete all needed tables and mockup data (few users and a task)
10. Start/Run the app


How to use the app:
1. In the main view you will see left section with buttons and list of tasks
2. On the right you will see details for the selected task
3. You select tasks by clicking (once) on the task in the list
4. First chart icon button is for opening a dasboard
5. Second icon button (search button) is for searching comments by any field. When you double-click on a comment a view with the corresponding task will appear.
6. Next to the Title "Tasks" there is a plus icon buton. With this button you will be able to create new tasks.
7. On the right panel you see information about the selected task and list with its comments.
8. For each comment you have a button to: see its history of changes, edit teh comment, delete the comment
9. Next to the Title "Comments" you have a button "Add Comment" where you can create new comments for the task
