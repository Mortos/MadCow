﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MadCow
{

    public partial class Form1 : Form
    {
        //Timing
        private int tik;
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        //-------------------------//
        // Unused Items in Form //
        //-------------------------//
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // Set up the ToolTip text for the Buttons.
            toolTip1.SetToolTip(this.button2, "This will update mooege to latest version");
            toolTip1.SetToolTip(this.button3, "This will copy MPQ's if you have D3 installed");
            toolTip1.SetToolTip(this.button8, "This will check pre-requirements and update Mooege Server");
            textBox4.Text = @"C:\Program Files (x86)\Diablo III Beta\Diablo III.exe";
        }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) { }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) { }
        private void toolTip1_Popup(object sender, PopupEventArgs e) { }
        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }

        //-------------------------//
        // Update Mooege //
        //-------------------------//
        private void button1_Click_Validate_Repository(object sender, EventArgs e)
        {
            //Update Mooege - does not start Diablo
            ParseRevision.revisionUrl = textBox1_Repository_Url.Text;  
            try
                {
                    ParseRevision.GetRevision();

                    if (ParseRevision.commitFile == "ConnectionFailure")
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        button2.Enabled = false;
                        label1.Text = "Internet Problems.";
                    }

                    else if (ParseRevision.commitFile == "Incorrect repository entry")
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = ParseRevision.errorSender;
                        pictureBox1.Show();
                        button2.Enabled = false;
                        label1.Text = "Please try a different Repo.";
                    }
                    
                    else if (ParseRevision.revisionUrl.EndsWith("/"))
                    {
                        pictureBox2.Hide();
                        textBox1_Repository_Url.Text = "Incorrect repository entry";
                        pictureBox1.Show();
                        button2.Enabled = false;
                        label1.Text = "Delete the last '/' on the repo.";
                    }
                    else

                    {
                        pictureBox2.Show();
                        textBox1_Repository_Url.ForeColor = Color.Green;
                        textBox1_Repository_Url.Text = ParseRevision.revisionUrl;
                        ParseRevision.getDeveloperName();
                        ParseRevision.getBranchName();
                        button2.Enabled = true;
                        label1.Text = "Repository Validated!";
                    }
                }
                catch (Exception)
                {
                    pictureBox2.Hide();
                    textBox1_Repository_Url.Text = ParseRevision.errorSender;
                    pictureBox1.Show();
                }
        }

        private void button2_Click_Update_Mooege(object sender, EventArgs e)
        {
            if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
            {
                label1.Text = "You have latest [" + ParseRevision.developerName + "] Mooege revision: " + ParseRevision.lastRevision;
            }

            else if (Directory.Exists(Program.programPath + "/MPQ")) //Checks for MPQ Folder
            {
                label1.Text = "Found default MadCow MPQ folder";
                button2.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }

            else
            {
                label1.Text = "Creating MadCow MPQ folder...";
                Directory.CreateDirectory(Program.programPath + "/MPQ");
                button2.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }


        //-------------------------//
        // Play Diablo Button      //
        //-------------------------//
        private void button4_Click(object sender, EventArgs e)
        {
            //TODO: waiting time between mooege starting up and diablo?

            //Compile.currentMooegeExePath = Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\Debug\Mooege.exe";
            //Process proc0 = new Process();
            //proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
            //proc0.Start();
            //MessageBox.Show(Compile.currentMooegeExePath);
            Process proc1 = new Process();
            proc1.StartInfo = new ProcessStartInfo(textBox4.Text);
            proc1.StartInfo.Arguments = " -launch -auroraaddress localhost:1345";
            proc1.Start();
            label1.Text = "Starting Diablo..";
            
        }


        //-------------------------//
        // Update MPQS             //
        //-------------------------//

        private void button3_Click(object sender, EventArgs e)
        {
            //Commands.RunUpdateMPQ();
        }


        //-------------------------//
        // Remote Server Settings  //
        //-------------------------//
        private void button7_Click(object sender, EventArgs e)
        {
            //Remote Server
            //Opens Diablo with extension to Remote Server
            Process proc1 = new Process();
            proc1.StartInfo = new ProcessStartInfo(textBox4.Text);
            String HostIP = textBox2.Text;
            String Port = textBox3.Text;
            String ServerHost = HostIP + @":" + Port;
            proc1.StartInfo.Arguments = @" -launch -auroraaddress " + ServerHost;
            MessageBox.Show(proc1.StartInfo.Arguments);
            //proc1.Start();
            label1.Text = "Starting Diablo..";
            
        }


        //-------------------------//
        // Server Control Settings //
        //-------------------------//
        private void textBox13_TextChanged(object sender, EventArgs e) { /*Bnet Server IP*/ }
        private void textBox12_TextChanged(object sender, EventArgs e) { /*Bnet Server Port*/ }
        private void textBox11_TextChanged(object sender, EventArgs e) { /*Game Server IP*/ }
        private void textBox10_TextChanged(object sender, EventArgs e) { /*Game Server Port*/ }
        private void textBox9_TextChanged(object sender, EventArgs e) { /*Public Server IP*/ }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { /*enable or disable NAT*/ }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Restore Default Server Control Settings
            textBox13.Text = "0.0.0.0";
            textBox12.Text = "1345";
            textBox11.Text = "0.0.0.0";
            textBox10.Text = "1999";
            textBox9.Text = "0.0.0.0";
            checkBox3.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //launches only mooege server
            //Any changes made on Server Control -> need to change config.ini
            /*
            Process proc0 = new Process();
            proc0.StartInfo = new ProcessStartInfo(Compile.currentMooegeExePath);
            proc0.Start();
            */
            MessageBox.Show(Compile.currentMooegeExePath);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Updates Mooege does not check for Diablo Client
            //MadCowRunProcedure.RunMadCow(0);
        }

        //-------------------------//
        // Timer Stuff //
        //-------------------------//
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                tik = (int)this.numericUpDown1.Value * 60;
                timer1.Start();
            }
            else
                timer1.Stop();
               label1.Text = " ";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
           tik--;
           if (tik == 0)
           {
                label1.Text = "Checking..";
                timer1.Stop();
                Commands.RunUpdate();
                tik = (int)this.numericUpDown1.Value * 60;
                timer1.Start();
           }
           else
               label1.Text = "Updating in " + tik.ToString();
        }
        
        //-------------------------//
        // Diablo 3 Path Stuff     //
        //-------------------------//
        private void button9_Click(object sender, EventArgs e)
        {
            //Opens path to find Diablo3
            OpenFileDialog d3folder = new OpenFileDialog();
            d3folder.Title = "Diablo 3.exe";
            d3folder.InitialDirectory = @"C:\Program Files (x86)\Diablo III Beta\";
            if (d3folder.ShowDialog() == DialogResult.OK) // Test result.
            {
                // Get the directory name.
                string dirName = System.IO.Path.GetDirectoryName(d3folder.FileName);
                // Output Name
                textBox4.Text = d3folder.FileName;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e) { /*Diablo Path*/ }

        //DOWNLOAD SOURCE FROM REPOSITORY
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {     
            Uri url = new Uri(ParseRevision.revisionUrl + "/zipball/master");
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();
            // Gets bytes.
            Int64 iSize = response.ContentLength;

            // Keeping track of downloaded bytes.
            Int64 iRunningByteTotal = 0;

            // Open Webclient.
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
            // Open the file at the remote path.
            using (System.IO.Stream streamRemote = client.OpenRead(new Uri(ParseRevision.revisionUrl + "/zipball/master")))
            {
            // We write those files into the file system.
                 using (Stream streamLocal = new FileStream(Program.programPath + "/Mooege.zip", FileMode.Create, FileAccess.Write, FileShare.None))
                 {
                  // Loop the stream and get the file into the byte buffer
                  int iByteSize = 0;
                  byte[] byteBuffer = new byte[iSize];
                  while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                  {
                  // Write the bytes to the file system at the file path specified
                      streamLocal.Write(byteBuffer, 0, iByteSize);
                      iRunningByteTotal += iByteSize;

                      // Calculate the progress out of a base "100"
                      double dIndex = (double)(iRunningByteTotal);
                      double dTotal = (double)byteBuffer.Length;
                      double dProgressPercentage = (dIndex / dTotal);
                      int iProgressPercentage = (int)(dProgressPercentage * 100);

                      // Update the progress bar
                      backgroundWorker1.ReportProgress(iProgressPercentage);
                   }

                   // Clean up the file stream
                   streamLocal.Close();
                   }

                // Close the connection to the remote server
                streamRemote.Close();
             }
          }
            
        }

        //UPDATE PROGRESS BAR
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
        }

        //PROCEED WITH THE PROCESS ONCE THE DOWNLOAD ITS COMPLETE
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Commands.RunUpdate();
        }

        //URL TEXT FIELD COLOR MANAGEMENT
        private void textBox1_Repository_Url_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                if (textBox1_Repository_Url.Text == "Incorrect repository entry." || textBox1_Repository_Url.Text == "Check your internet connection.")
                {
                    textBox1_Repository_Url.ForeColor = Color.Red;
                    //this.label4.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    textBox1_Repository_Url.ForeColor = Color.Black;
                    this.label4.BackColor = System.Drawing.Color.Transparent;
                    pictureBox1.Hide();//Error
                    pictureBox2.Hide();//Correct
                }
            }
            catch
            {
                textBox1_Repository_Url.ForeColor = SystemColors.ControlText;
            }
        }
       //----------------------------------------------------------------------
        /*private void Labels(object sender, EventArgs e)
        {

                if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision))
                {
                    // This path is a file
                    label1.Text = "Uncompress Done";
                }
                else if (Directory.Exists(Program.programPath + @"\" + ParseRevision.developerName + "-" + ParseRevision.branchName + "-" + ParseRevision.lastRevision + @"\src\Mooege\bin\"))
                {
                    // This path is a directory
                    label1.Text = "Compile Complete";
                }
                else if (Directory.Exists(Program.programPath + "/MPQ"))
                {
                    label1.Text = "MPQ folder Created";
                }
                else if (Directory.Exists(Program.programPath + "/MPQ/base") && File.Exists(Program.programPath + "/MPQ/CoreData.mpq") && File.Exists(Program.programPath + "/MPQ/ClientData.mpq"))
                {
                    label1.Text = "Copying MPQs Complete";
                }
                else
                {
                    label1.Text = "";
                }
        }
         */
    }

 }
