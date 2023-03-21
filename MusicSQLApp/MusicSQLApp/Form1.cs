namespace MusicSQLApp
{
    public partial class Form1 : Form


        

    {
        BindingSource albumBindingSource = new BindingSource();
        BindingSource tracksBindingSource = new BindingSource();
        List<Album> albums = new List<Album>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();



            //Connect the List to the grid view control

            albums = albumsDAO.getAllAlbums();

            albumBindingSource.DataSource = albums;
            dataGridView1.DataSource = albumBindingSource;


            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();



            //Connect the List to the grid view control

            albumBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);
            dataGridView1.DataSource = albumBindingSource;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            //Get row number
            int rowClicked = dataGridView.CurrentRow.Index;
            //int colClicked = dataGridView.CurrentCell.ColumnIndex;

            string imageUrl = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();
            //string videoUrl = dataGridView.Rows[rowClicked].Cells[3].Value.ToString();

            //MessageBox.Show("URL = " + imageUrl);

            pictureBox1.Load(imageUrl);

            //AlbumsDAO albumsDAO=new AlbumsDAO();

            //tracksBindingSource.DataSource = albumsDAO.getTracksUsingJoin((int)dataGridView.Rows[rowClicked].Cells[0].Value);


            tracksBindingSource.DataSource = albums[rowClicked].Tracks;

            dataGridView2.DataSource = tracksBindingSource;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Add a new item to the database
            Album album = new Album
            {
                AlbumName = txt_albumname.Text,
                ArtistName = txt_Artist.Text,
                Year = Int32.Parse(txt_Year.Text),
                ImageURL = txt_imageURL.Text,
                Description = txt_description.Text,

            };

            AlbumsDAO albumsDAO = new AlbumsDAO();
            int result = albumsDAO.addOneAlbum(album); //tells us whether insert was corect or not 
            MessageBox.Show(result + "new row(s) inserted");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Get row number
            int rowClicked = dataGridView2.CurrentRow.Index;
            //int colClicked = dataGridView.CurrentCell.ColumnIndex;

            string imageUrl = dataGridView2.Rows[rowClicked].Cells[4].Value.ToString();

            MessageBox.Show("Lyrics = " + imageUrl);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Get row number
            int rowClicked = dataGridView2.CurrentRow.Index;
            

            MessageBox.Show("You Clicked Row = " + rowClicked);
            int trackID = (int) dataGridView2.Rows[rowClicked].Cells[0].Value;
            MessageBox.Show("ID of track = " + trackID);

            AlbumsDAO albumsDAO=new AlbumsDAO();
            int result = albumsDAO.deleteTrack(trackID);
            MessageBox.Show("Result" + result);


            dataGridView2.DataSource = null;
            albums = albumsDAO.getAllAlbums();

        }
    }
} 