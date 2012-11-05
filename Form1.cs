using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChangeWatcher {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        public FileSystemWatcher watcher;
        public delegate void ArgumentChanged(string text);
        public delegate void ArgumentCreated(string text);
        public delegate void ArgumentDeleted(string text);
        public delegate void ArgumentRenamed(string text);
        public ArgumentChanged Changed;
        public ArgumentCreated Created;
        public ArgumentDeleted Deleted;
        public ArgumentRenamed Renamed;


        private void textBox1_TextChanged(object sender, EventArgs e) {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (this.checkBox1.Text == "Start") {
                subscribe();
                this.checkBox1.Text = "Stop";
            } else {
                unsubscribe();
                this.checkBox1.Text = "Start";
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            watcher = new FileSystemWatcher();
            watcher.Path = System.IO.Path.GetPathRoot(System.Environment.CurrentDirectory);
            watcher.Filter = "*";
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = (NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            Changed = new ArgumentChanged(Ch);
            Created = new ArgumentCreated(Cr);
            Deleted = new ArgumentDeleted(Dl);
            Renamed = new ArgumentRenamed(Rn);
        }

        public void OnChanged(object sender, FileSystemEventArgs e) {
            string data = DateTime.Now + " | " + "Changed" + " | " + "File: " + " | " + e.Name + "\n";
            this.Invoke(Changed, data);
        }
        public void OnCreated(object sender, FileSystemEventArgs e) {
            string data = DateTime.Now + " | " + "Created" + " | " + "File: " + " | " + e.Name + "\n";
            this.Invoke(Created, data);
        }
        public void OnDeleted(object sender, FileSystemEventArgs e) {
            string data = DateTime.Now + " | " + "Deleted" + " | " + "File: " + " | " + e.Name + "\n";
            this.Invoke(Deleted, data);
        }
        public void OnRenamed(object sender, RenamedEventArgs e) {
            string data = DateTime.Now + " | " + "Renamed" + " | " + "File: " + " | " + e.Name + "\n";
            this.Invoke(Renamed, data);
        }

        private void Ch(string data) { this.textBox1.AppendText(data); }
        private void Cr(string data) { this.textBox1.AppendText(data); }
        private void Dl(string data) { this.textBox1.AppendText(data); }
        private void Rn(string data) { this.textBox1.AppendText(data); }

        private void subscribe() {
            watcher.EnableRaisingEvents = true;
        }

        private void unsubscribe() {
            watcher.EnableRaisingEvents = false;
        }
    }
}
