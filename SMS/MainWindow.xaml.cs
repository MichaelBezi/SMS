using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace SMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //To declare variables
        Decimal subpay = 10000, subcent = 100, subtot = 0;
        int log = 0, pw = 0, qstat, lsmenu, subdur = 0;
        Visibility hi, sh;
        string pword, lev, q1, q2, q3, q4, qp, btnp, msg, fpath1, fpath2, fpath3, p1, p2, p3;
        System.Windows.Threading.DispatcherTimer timprog = new System.Windows.Threading.DispatcherTimer();
        WPFCSharpWebCam.WebCam webcam;
        WPFCSharpWebCam.WebCam1 studcam;
        WPFCSharpWebCam.WebCam2 staffcam;
        
        public MainWindow()
        {
            InitializeComponent();
            timprog.Tick += timprog_Tick;
            timprog.Interval = TimeSpan.FromMilliseconds(1);
        }

        private async System.Threading.Tasks.Task ComputeNextMove()
        {
            // Perform background work here.
            /* The qp is the query purpose
            1. s = select statement and read
            2. si = select statement, read and insert
            3. sii = select statement, read, insert and insert
            4. u = update statement
            5. su = select, read and update statement
            6. d = delete statement
            7. gs = select and display in datagrid
            8. siu = select statement, read, insert and update
            */
            // Don't directly access UI elements from this method.
            qstat = 0;
            try
            {
                switch (qp)
                {
                    case "siu":
                        qstat = 1;
                        break;
                    case "si":
                        qstat = 1;
                        break;
                    default:
                        break;
                    case "s":
                        qstat = 1;
                        break;
                    case "gs":
                        qstat = 1;
                        break;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }
        private async void NextMove_Click(object sender, RoutedEventArgs e)
        {
            // The await causes the handler to return immediately.
            lblerror.Visibility = hi;
            lblmsg.Visibility = hi;
            picload.Visibility = sh;
            picload2.Visibility = sh;
            await System.Threading.Tasks.Task.Run(() => ComputeNextMove());
            // Now update the UI with the results.
            try
            {
                if (qstat == 1)
                {
                    switch (qp)
                    {
                        case "siu":
                            switch (btnp)
                            {
                                case "login":
                                    hi_form(sender, e);
                                    log = 1;
                                    hi_form(sender, e);
                                    lstmenu.SelectedIndex = 0;
                                    //To clear the sigup and login forms
                                    txtuser.Clear();
                                    txtpword.Clear();
                                    txtregus.Clear();
                                    txtregph.Clear();
                                    txtregfname.Clear();
                                    txtregaddr.Clear();
                                    txtregpw.Clear();
                                    txtregmail.Clear();
                                    break;
                            }
                            break;
                        case "si":
                            switch (btnp)
                            {
                                case "signup":
                                    txtuser.Text = "fjefe";
                                    txtpword.Password = "ekfjel";
                                    btnlogin_Click(sender, e);
                                    break;
                            }
                            break;
                        case "s":
                            switch (btnp)
                            {
                                case "profile":
                                    tabmain.SelectedIndex = lsmenu;
                                    break;
                            }
                            break;
                        case "gs":
                            switch (btnp)
                            {
                                case "students":
                                    tabmain.SelectedIndex = lsmenu;
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    tabmain.Visibility = sh;
                }
                else
                {
                    lblmsg.Content = msg; lblmsg.Visibility = sh;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tabmain.Visibility = hi;
                lblerror.Visibility = sh;
            }
            picload.Visibility = hi;
            picload2.Visibility = hi;
        }
        //To calculate subscriptions
        private void sub_calc(object sender, RoutedEventArgs e)
        {
            //Variables from the price list
            switch (subdur)
            {
                case 0:
                    lblsub.Content = "Select a valid package...";
                    break;
                default:
                    lblsub.Content = "#" + Convert.ToString(subdur *(subpay * subcent/100));
                    break;
            }

        }
        private void hi_form(object sender, RoutedEventArgs e)
        {
            //To hide some elements of the program
            switch (log)
            {
                //Before login
                case 1:
                    gdmenu.Visibility = sh;
                    txtabt.Visibility = sh;
                    gdlog.Visibility = hi;
                    gdsign.Visibility = hi;
                    picload.Visibility = hi;
                    gdmenu.Height = this.Height;
                    gdmenu.Width = this.Width;
                    txtabt.Visibility = hi;
                    switch (lev)
                    {
                        case "admin":
                            btnad.Visibility = sh;
                            break;
                        default:
                            btnad.Visibility = hi;
                            tabadmin.Visibility = hi;
                            break;
                    }
                    break;
                default:
                    txtabt.Visibility = sh;
                    gdlog.Visibility = hi;
                    gdsign.Visibility = hi;
                    gdmenu.Visibility = hi;
                    break;
            }
        }

        private void txtuser_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show/hide the hint
            switch (txtuser.Text.Length)
            {
                case 0:
                    lbluser.Content = "Username*";
                    break;
                default:
                    lbluser.Content = "";
                    break;
            }
        }

        private void timprog_Tick(object sender, EventArgs e)
        {
            //To show/hide the hint
            switch (pw)
            {
                case 1:
                    pword = txtpword.Password;
                    switch (pword.Length)
                    {
                        case 0:
                            lblpword.Content = "Password*";
                            break;
                        default:
                            lblpword.Content = "";
                            break;
                    }
                    break;
                default:

                    break;
            }
           /* gdmenu.Width = gdmain.Width;
            gdmenu.Height = gdmain.Height;*/
        }

        private void btnlog_Click(object sender, RoutedEventArgs e)
        {
            //To show the login form
            hi_form(sender, e);
            txtuser.Clear();
            txtpword.Clear();
            gdlog.Visibility = sh;
            lblpword.Content = "Password*";
            txtuser.Focus();
        }

        private void btnsign_Click(object sender, RoutedEventArgs e)
        {
            //To show the signup form
            hi_form(sender, e);
            txtregus.Clear();
            txtregph.Clear();
            txtregfname.Clear();
            txtregaddr.Clear();
            txtregpw.Clear();
            txtregmail.Clear(); gdsign.Visibility = sh;
        }

        private void btnrec_Click(object sender, RoutedEventArgs e)
        {
            //To recover password

        }

        private void txtregus_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show/hide the hint
            if (txtregus.Text.Length == 0)
            {
                lblregus.Content = "Username*";
            }
            else
            {
                lblregus.Content = "";
            }
            if (txtpop.Text.Length == 0)
            {
                lblpop.Content = "Old Password*";
            }
            else
            {
                lblpop.Content = "";
            }
            if (txtstus.Text.Length == 0)
            {
                lblstus.Content = "Username*";
            }
            else
            {
                lblstus.Content = "";
            }
        }

        private void txtregpw_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show/hide the hint
            switch (txtregpw.Text.Length + txtpnp.Text.Length + txtstpword.Text.Length)
            {
                case 0:
                    lblregpw.Content = "Password*";
                    lblpnp.Content = "New Password*";
                    lblstpword.Content = "Password*";
                    break;
                default:
                    lblregpw.Content = "";
                    lblpnp.Content = "";
                    lblstpword.Content = "";
                    break;
            }
        }

        private void txtregfname_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show/hide the hint
            if (txtregfname.Text.Length == 0)
            {
                lblregfname.Content = "Full Name*";
            }
            else
            {
                lblregfname.Content = "";
            }
            if (txtpfname.Text.Length == 0)
            {
                lblpfname.Content = "Full Name*";
            }
            else
            {
                lblpfname.Content = "";
            }
            if (txtstfname.Text.Length == 0)
            {
                lblstfname.Content = "Full Name*";
            }
            else
            {
                lblstfname.Content = "";
            }
            //School Name* (Optional... for admin only)
            if (txtregschname.Text.Length == 0)
            {
                lblregschname.Content = "School Name* (Optional... for admin only)";
            }
            else
            {
                lblregschname.Content = "";
            }
        }

        private void txtregph_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show/hide the hint
            if (txtregph.Text.Length == 0)
            {
                lblregph.Content = "Phone No.*";
            }
            else
            {
                lblregph.Content = "";
            }
            if (txtpph.Text.Length == 0)
            {
                lblpph.Content = "Phone No.*";
            }
            else
            {
                lblpph.Content = "";
            }
            if (txtstph.Text.Length == 0)
            {
                lblstph.Content = "Phone No.*";
            }
            else
            {
                lblstph.Content = "";
            }
        }

        private void cbotrantpe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //To show the list of items
            try
            {
                switch (cbotrantpe.SelectedIndex)
                {
                    case 1:
                        lblthead.Content = "List of students";
                        break;
                    case 2:
                        lblthead.Content = "List of staff";
                        break;
                    case 3:
                        lblthead.Content = "Expenses";
                        break;
                    case 4:
                        lblthead.Content = "Funds";
                        break;
                    case 0:
                        lblthead.Content = "Transactions";
                        break;
                }
            }
            catch (Exception)
            {

            }
        }

        private void ts_clear(object sender, RoutedEventArgs e)
        {
            //To clear the transaction field on got focus
            if (txttransch.Text == "Search                                                                                         🔍")
            {
                txttransch.Clear();
            }
        }

        private void hide_menpcam(object sender, MouseEventArgs e)
        {
            //To hide the pcam effect
            menpcam.Visibility = hi;
            menscam.Visibility = hi;
            menstcam.Visibility = hi;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //To start the cam feed
            webcam.Start();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //To capture the feed
            webcam.Stop();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            //To configure the webcam
            webcam.AdvanceSetting();
        }

        private void btnstcam_Click(object sender, RoutedEventArgs e)
        {
            //To start the staff cam
            menstcam.Visibility = sh;
        }
        private void ts_ls(object sender, RoutedEventArgs e)
        {
            //To clear the ts field on lost focus
            if (txttransch.Text.Length < 1)
            {
                txttransch.Text = "Search                                                                                         🔍";
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            studcam.Start();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            studcam.Stop();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            studcam.AdvanceSetting();
        }

        private void btnsfile_Click(object sender, RoutedEventArgs e)
        {
            //To get from file
            studcam.Stop();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                p1 = fileUri.ToString();
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = fileUri;
                image.EndInit();
                picstud.Source = image;
            }
        }

        private void btnstfile_Click(object sender, RoutedEventArgs e)
        {
            //To get from file
            staffcam.Stop();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                p1 = fileUri.ToString();
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = fileUri;
                image.EndInit();
                picstaff.Source = image;
            }
        }

        private void tot_calc(object sender, TextChangedEventArgs e)
        {
            //Tot calculate total score
            try
            {
                txttot.Text = Convert.ToString(Convert.ToDecimal(txtft.Text) +
                   Convert.ToDecimal( txtst.Text) + Convert.ToDecimal( txtex.Text));
            }
            catch (Exception)
            {

                
            }
        }

        private void btnmsg_Click(object sender, RoutedEventArgs e)
        {
            //To open messages
            tabmain.SelectedIndex = 6;
            tabinbox.SelectedIndex = 0;
        }

        private void sub1_Checked(object sender, RoutedEventArgs e)
        {
            //To change the cost
            subpay = 10000;
            sub_calc(sender, e);
        }

        private void sub2_Checked(object sender, RoutedEventArgs e)
        {
            //To change the cost
            subpay = 15000;
            sub_calc(sender, e);
        }

        private void sub3_Checked(object sender, RoutedEventArgs e)
        {
            //To change the cost
            subpay = 20000;
            sub_calc(sender, e);
        }

        private void sub4_Checked(object sender, RoutedEventArgs e)
        {
            //To change the cost
            subpay = 250000;
            sub_calc(sender, e);
        }

        private void btnad_Click(object sender, RoutedEventArgs e)
        {
            //Tab admin view
            tabmain.SelectedIndex = 7;
            admintab.SelectedIndex = 0;
        }

        private void cbosub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            //To do the subscription calculation
            switch (cbosub.SelectedIndex)
            {
                case 1:
                    subdur = 1;
                    subcent = 100;
                    break;
                case 2:
                    subdur = 3;
                    subcent = 95;
                    break;
                case 3:
                    subdur = 6;
                    subcent = 92;
                    break;
                case 4:
                    subdur = 12;
                    subcent = 90;
                    break;
                case 5:
                    subdur = 24;
                    subcent = 85;
                    break;
                case 6:
                    subdur = 36;
                    subcent = 80;
                    break;
                case 0:
                    subdur = 0;
                    lblsub.Content = "Select a valid package...";
                    break;
            }
            sub_calc(sender, e);
            }
            catch (Exception)
            {

            }
        }

        private void btnotf_Click(object sender, RoutedEventArgs e)
        {
            //To open notifications
            tabmain.SelectedIndex = 6;
            tabinbox.SelectedIndex = 1;
        }

        private void btnfeed_Click(object sender, RoutedEventArgs e)
        {
            //To open feedback
            tabmain.SelectedIndex = 6;
            tabinbox.SelectedIndex = 2;
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            staffcam.Start();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            staffcam.Stop();
        }

        private void btnpfile_Click(object sender, RoutedEventArgs e)
        {
            //To get from file
            webcam.Stop();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                p1 = fileUri.ToString();
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = fileUri;
                image.EndInit();
                picp.Source = image;
            }
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            staffcam.AdvanceSetting();
        }

        private void btnscam_Click(object sender, RoutedEventArgs e)
        {
            //Student capture
            menscam.Visibility = sh;
        }

        private void btnpcam_Click(object sender, RoutedEventArgs e)
        {
            //To show the cam menu
            menpcam.Visibility = sh;
        }

        private void tab_dash(object sender, SelectionChangedEventArgs e)
        {
            //To change selection
            int tab = tabmain.SelectedIndex - 1;
            if (tab < -1)
            {
                tab = -1;
            }
            if (tabmain.SelectedIndex == 0)
            {
                txtsearch.Clear();
            }
            else if (tabmain.SelectedIndex > 5)
            {
                lsmenu = 6;
                tab = -1;
            }
            lstmenu.SelectedIndex = tab;
        }

        private void txtregmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show/hide the hint
            if (txtregmail.Text.Length == 0)
            {
                lblregmail.Content = "Email: example@gmail.com";
            }
            else
            {
                lblregmail.Content = "";
            }
            if (txtppmail.Text.Length == 0)
            {
                lblpmail.Content = "Email: example@gmail.com";
            }
            else
            {
                lblregmail.Content = "";
            }

            if (txtstmail.Text.Length == 0)
            {
                lblstmail.Content = "Email: example@gmail.com";
            }
            else
            {
                lblstmail.Content = "";
            }
        }

        private void txtregaddr_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show/hide the hint
            switch (txtregaddr.Text.Length + txtpaddr.Text.Length)
            {
                case 0:
                    lblregaddr.Content = "Address*";
                    lblpaddr.Content = lblregaddr.Content;
                    break;
                default:
                    lblregaddr.Content = "";
                    lblpaddr.Content = "";
                    break;
            }
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //To show the search hint
            if (txtsearch.Text.Length > 0)
            {
                lblsearch.Content = "";
                q1 = "search query";
                qp = "gs";
                NextMove_Click(sender, e);
            }
            else
            {
                lblsearch.Content = "Search                                                                                                         🔍";
                dtgres.Items.Clear();
            }
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            //To login to the database
            if (txtuser.Text != "" && txtpword.Password != "")
            {
                lev = "user";
                q1 = "Query to select user";
                q2 = "insert log";
                q3 = "update user status";
                qp = "siu";
                btnp = "login";
                NextMove_Click(sender, e);
            }
        }

        private void btnsignup_Click(object sender, RoutedEventArgs e)
        {
            //To signup to the database
            if (txtregus.Text != "" && txtregpw.Text != "" && txtregmail.Text !="" && txtregph.Text != "" &&
                txtregfname.Text !="" && txtregaddr.Text != "")
            {
                q1 = "select query";
                q2 = "insert query";
                qp = "si";
                btnp = "signup";
                NextMove_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Please fill the form properly...", "Signup", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnadj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnlogad_Click(object sender, RoutedEventArgs e)
        {
            //To show the login as admin
            hi_form(sender, e);
            log = 1;
            lev = "admin";
            hi_form(sender, e);
            lstmenu.SelectedIndex = 0;
            //To clear the sigup and login forms
            txtuser.Clear();
            txtpword.Clear();
            txtregus.Clear();
            txtregph.Clear();
            txtregfname.Clear();
            txtregaddr.Clear();
            txtregpw.Clear();
            txtregmail.Clear();
            tabadmin.Visibility = sh;
        }

        private void lstmenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //To show the menu
            if (lsmenu < 6)
            {
                lsmenu = lstmenu.SelectedIndex + 1;
            }
            switch (lstmenu.SelectedIndex)
            {
                case 1:
                    //For students
                    qp = "gs";
                    q1 = "select query for students records";
                    btnp = "students";
                    NextMove_Click(sender, e);
                    break;
                case 5:
                    //For logout
                    log = 0;
                    hi_form(sender, e);
                    btnlog_Click(sender, e);
                    lstmenu.SelectedIndex = 0;
                    break;
                default:
                    //For profile
                    qp = "s";
                    q1 = "select profile to load details";
                    btnp = "profile";
                    NextMove_Click(sender, e);
                    break;
            }
        }

        private void pword_on(object sender, RoutedEventArgs e)
        {
            //To start the pword event
            pw = 1;
        }

        private void pword_off(object sender, RoutedEventArgs e)
        {
            //To stop the pword event
            pw = 0;
        }

        private void on_load(object sender, RoutedEventArgs e)
        {
            //To give variables defalult values
            log = 0;
            hi = Visibility.Hidden;
            sh = Visibility.Visible;
            btnlog_Click(sender, e);
            timprog.Start();
            btnadj.Visibility = hi;
            gdmain.Margin = btnadj.Margin;
            gdmain.Width = 1280;
            gdmain.Height = 700;
            gdmenu.Margin = btnadj.Margin;
            btnad.Visibility = hi;
            picload.Visibility = hi;
            // TODO: Add event handler implementation here.
            webcam = new WPFCSharpWebCam.WebCam();
            webcam.InitializeWebCam(ref picp);
            studcam = new WPFCSharpWebCam.WebCam1();
            studcam.InitializeWebCam(ref picstud);
            staffcam = new WPFCSharpWebCam.WebCam2();
            staffcam.InitializeWebCam(ref picstaff);
            menpcam.Visibility = hi;
            menscam.Visibility = hi;
            menstcam.Visibility = hi;
        }
    }
}
