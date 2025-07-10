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

Known issues:
1. When creating a comment the page does not reload, so you have to move to another task and back to see the new comment. I added even + listener, but there was sime issue and I did not have enough time to fix it.
2. Comment create takes more time than it should (at least in my environment)
3. Comment does not update the task NextAction date. I did not have enough time to add the proper implementation. What I was planning was to add check (when saving a comment) if the NextAction date is not null and NextActionDate is null or is later than NextAction date and to invoce an update only on the property NextActionDate for the task.
4. When no task is selected I should hide the Task Detail section (I have a helper converter, but there was an issue and I did not have the time to track it properly).
5. Dashboard style is very bad. Need butification (a lot)
6. Code needs refactoring - there is a lot of code that is not needed and can be removed. There are a few methods that are repetative and can be done in one generic approach. There are some unnecessary "reloads" that can be optimized. I did not have enough time to fix that.
