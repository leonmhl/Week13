using System.Data.SQLite;

ReadData(CreateConnection());
//AddCustomer(CreateConnection());
RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version=3; New=true; Compress=True;");

    try
    {
        connection.Open();
        Console.WriteLine("Connection established");
    }
    catch
    {
        Console.WriteLine("DB connection failed");
    }

    return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    SQLiteDataReader read;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "Select * FROM customer";
    read = command.ExecuteReader();

    while (read.Read())
    {
        string fName = read.GetString(0);
        string lNAme = read.GetString(1);
        string dob = read.GetString(2);

        Console.WriteLine($"Full name: {fName} {lNAme}; DoB: {dob}");
    }
    myConnection.Close();
}

static void AddCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    String fName = "Harry";
    string lName = "Potter";
    string dob = "07-31-1980";

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) VALUES('{fName}', '{lName}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery();

    Console.Clear();
    Console.WriteLine($"Rows inserted: {rowInserted}");

  

    ReadData(myConnection);

    myConnection.Close();

}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete = "10";

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";

    int rowDeleted = command.ExecuteNonQuery();

    Console.Clear();
    Console.WriteLine($"Rows deleted: {rowDeleted}");

    ReadData(myConnection);

    myConnection.Close();
}