using djvu.models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SQLite;



namespace djvu
{
    public partial class Setup : Form
    {
        public string dbname = "ETIMS";

        public Setup()
        {
            InitializeComponent();
            InitializeDatabase(dbname);
            this.Load += Setup_Load;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string pin = textBox1.Text;
            string code = textBox2.Text;
            string[]? error = null;
            if (string.IsNullOrEmpty(pin))
            {
                error = new string[] { "Pin can not be empty" };
            }
            if (string.IsNullOrEmpty(code))
            {
                error = new string[] { "Authorization code can not be empty" };
            }

            // int length = error.Length;
            if (error != null)
            {
                // 
                MessageBox.Show(error[0], "Some title",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                await CallApi(pin, code);
            }

        }

        public async Task CallApi(string param1, string param2)
        {
            using (var client = new HttpClient())
            {
                var apiUrl = "https://dejavutechkenya.com/crm/api/verifyDevice.php";
                var url = $"{apiUrl}?pin={param1}&key={param2}";

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    
                    var response = await client.GetAsync(url);
                    // Do something with the response...

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Activation>(json);

                        if (result.status == "ACTIVE")
                        {

                            AddAuthRecord(dbname, param1, param2, DateTime.Now);

                            Console.WriteLine($"Value of parameter 3: {result.status}");
                            Initiolize initiolize = new Initiolize();
                            this.Cursor = Cursors.Default;
                            this.Hide();
                            initiolize.Show();
                        }
                        else
                        {
                            MessageBox.Show("Wrong information", "Connection Error",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Handle exception related to network connectivity or DNS resolution
                    Console.WriteLine($"An error occurred while sending the request: {ex.Message}");
                    this.Cursor = Cursors.Default;
                    MessageBox.Show($"Could Not connect to Server : {ex.Message}", "Connection Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    // Handle any other type of exception
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Could Not connect to Server", "Connection Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
        }

        private object InitializeDatabase(string dbName)
        {
            string dbPath = $"{dbName}.db";
            if (!File.Exists(dbPath))
            {
                Console.WriteLine("am here ......");
                SQLiteConnection.CreateFile(dbPath);

                CreateTableAuth(dbName);
                CreateTableConn(dbName);
                CreateTableMapping(dbName);

                //InitializeComponent();
            }
            else
            {
               
               
                
            }
            return null;
        }

        private SQLiteConnection GetConnection(string dbName)
        {
            string connectionString = $"Data Source={dbName}.db;Version=3;";
            return new SQLiteConnection(connectionString);
        }

        private void CreateTableAuth(string dbName)
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Auth (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Pin TEXT NOT NULL,
                    AuthKey TEXT NOT NULL,
                    Cdate DATE NOT NULL
                );";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        private void CreateTableConn(string dbName)
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Conn (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Pin TEXT NOT NULL,
                    bhfId TEXT NOT NULL,
                    dvcSrlNo TEXT NOT NULL,
                    connType TEXT NOT NULL,
                    link TEXT NOT NULL,
                    temp TEXT ,
                    Psize TEXT ,
                    Cdate DATE NOT NULL
                );";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }



        private void CreateTableMapping(string dbName)
        {
            string createTableQuery = @"
           CREATE TABLE IF NOT EXISTS Documents (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Temp TEXT NOT NULL,
            DocumentPin TEXT NOT NULL,
            InvoiceNumber TEXT NOT NULL,
            CustomerPin TEXT NOT NULL,
            DocumentType TEXT NOT NULL,
            ItemDescription TEXT NOT NULL,
            TaxType TEXT NOT NULL,
            ItemCode TEXT NOT NULL,
            Qty INTEGER NOT NULL,
            Price REAL NOT NULL,
            SaleAmount REAL NOT NULL,
            TaxableAmount REAL NOT NULL,
            TotalTax REAL NOT NULL,
            TotalAmount REAL NOT NULL,
            QRLocation TEXT NOT NULL,
            TaxAmtA REAL,
            TaxAmtB REAL,
            TaxAmtC REAL,
            TaxAmtD REAL,
            TaxAmtE REAL
            );";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        //

        private void AddDocument(string dbName, string temp, string documentPin, string invoiceNumber, string customerPin, string documentType, string itemDescription, string taxType, string itemCode, int qty, double price, double saleAmount, double taxableAmount, double totalTax, double totalAmount, string qrLocation, double taxAmtA, double taxAmtB, double taxAmtC, double taxAmtD, double taxAmtE)
        {
            string insertDocumentQuery = @"
    INSERT INTO Documents (Temp, DocumentPin, InvoiceNumber, CustomerPin, DocumentType, ItemDescription, TaxType, ItemCode, Qty, Price, SaleAmount, TaxableAmount, TotalTax, TotalAmount, QRLocation, TaxAmtA, TaxAmtB, TaxAmtC, TaxAmtD, TaxAmtE) 
    VALUES (@Temp, @DocumentPin, @InvoiceNumber, @CustomerPin, @DocumentType, @ItemDescription, @TaxType, @ItemCode, @Qty, @Price, @SaleAmount, @TaxableAmount, @TotalTax, @TotalAmount, @QRLocation, @TaxAmtA, @TaxAmtB, @TaxAmtC, @TaxAmtD, @TaxAmtE);";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(insertDocumentQuery, connection))
                {
                    command.Parameters.AddWithValue("@Temp", temp);
                    command.Parameters.AddWithValue("@DocumentPin", documentPin);
                    command.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);
                    command.Parameters.AddWithValue("@CustomerPin", customerPin);
                    command.Parameters.AddWithValue("@DocumentType", documentType);
                    command.Parameters.AddWithValue("@ItemDescription", itemDescription);
                    command.Parameters.AddWithValue("@TaxType", taxType);
                    command.Parameters.AddWithValue("@ItemCode", itemCode);
                    command.Parameters.AddWithValue("@Qty", qty);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@SaleAmount", saleAmount);
                    command.Parameters.AddWithValue("@TaxableAmount", taxableAmount);
                    command.Parameters.AddWithValue("@TotalTax", totalTax);
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@QRLocation", qrLocation);
                    command.Parameters.AddWithValue("@TaxAmtA", taxAmtA);
                    command.Parameters.AddWithValue("@TaxAmtB", taxAmtB);
                    command.Parameters.AddWithValue("@TaxAmtC", taxAmtC);
                    command.Parameters.AddWithValue("@TaxAmtD", taxAmtD);
                    command.Parameters.AddWithValue("@TaxAmtE", taxAmtE);
                    command.ExecuteNonQuery();
                }
            }
        }

            //

            private DataTable GetDocuments(string dbName)
        {
            string selectDocumentsQuery = "SELECT * FROM Documents;";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(selectDocumentsQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable documentsTable = new DataTable();
                        adapter.Fill(documentsTable);
                        return documentsTable;
                    }
                }
            }
        }
        //

        public bool HasRecords(string dbName, string tableName)
        {
            string countRecordsQuery = $"SELECT COUNT(*) FROM {tableName};";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(countRecordsQuery, connection))
                {
                    int recordCount = Convert.ToInt32(command.ExecuteScalar());
                    return recordCount > 0;
                }
            }
        }


        private void AddAuthRecord(string dbName, string pin, string authKey, DateTime cdate)
        {
            string insertAuthRecordQuery = @"
            INSERT INTO Auth (Pin, AuthKey, Cdate) 
            VALUES (@Pin, @AuthKey, @Cdate);";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(insertAuthRecordQuery, connection))
                {
                    command.Parameters.AddWithValue("@Pin", pin);
                    command.Parameters.AddWithValue("@AuthKey", authKey);
                    command.Parameters.AddWithValue("@Cdate", cdate);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void AddConnRecord(string dbName, string pin, string bhfId, string dvcSrlNo, string connType, string link, string temp, string psize, DateTime cdate)
        {
            string insertConnRecordQuery = @"
            INSERT INTO Conn (Pin, bhfId, dvcSrlNo, connType, link, temp, Psize, Cdate)
            VALUES (@Pin, @bhfId, @dvcSrlNo, @connType, @link, @temp, @Psize, @Cdate);";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(insertConnRecordQuery, connection))
                {
                    command.Parameters.AddWithValue("@Pin", pin);
                    command.Parameters.AddWithValue("@bhfId", bhfId);
                    command.Parameters.AddWithValue("@dvcSrlNo", dvcSrlNo);
                    command.Parameters.AddWithValue("@connType", connType);
                    command.Parameters.AddWithValue("@link", link);
                    command.Parameters.AddWithValue("@temp", temp);
                    command.Parameters.AddWithValue("@Psize", psize);
                    command.Parameters.AddWithValue("@Cdate", cdate);

                    command.ExecuteNonQuery();
                }
            }
        }


        private void Setup_Load(object sender, EventArgs e)
        {
            if (HasRecords(this.dbname, "Auth"))
            {
                Initiolize initiolize = new Initiolize();
                initiolize.Shown += (s, args) => this.Hide();
                initiolize.Show();
            }
        }

        public void UpdateTempAndPsize(string dbName, int id, string newTemp, string newPsize)
        {
            string updateTempAndPsizeQuery = @"
        UPDATE Conn
        SET temp = @NewTemp, Psize = @NewPsize
        WHERE Id = @Id;";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(updateTempAndPsizeQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@NewTemp", newTemp);
                    command.Parameters.AddWithValue("@NewPsize", newPsize);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool TempAndPsizeHaveValues(string dbName, int id)
        {
            string checkTempAndPsizeQuery = @"
        SELECT temp, Psize
        FROM Conn
        WHERE Id = @Id;";

            using (SQLiteConnection connection = GetConnection(dbName))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(checkTempAndPsizeQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string temp = reader["temp"].ToString();
                            string psize = reader["Psize"].ToString();

                            return !string.IsNullOrEmpty(temp) && !string.IsNullOrEmpty(psize);
                        }
                    }
                }
            }

            return false;
        }






    }
}