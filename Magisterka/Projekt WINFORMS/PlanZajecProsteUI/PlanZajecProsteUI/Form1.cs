using PlanZajecProsteUI.Code;
using PlanZajecProsteUI.Data;
using PlanZajecProsteUI.Models;
using PlanZajecProsteUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanZajecProsteUI
{
    public partial class Form1 : Form
    {
        PlanDBContext context = new PlanDBContext();
        SolutionManager sm = new SolutionManager();
        public Form1()
        {
            InitializeComponent();


            RefreshComboBoxData();
        }

        void LoadDataView(int classID, Instance instance)
        {
            List<PlanViewModel> plan = new List<PlanViewModel>();
            List<TimeGroup> days = instance.TimeGroups.Where(x => x.Type == TimeGroupsType.Day).ToList();
            List<Time> pon = instance.Times.Where(x => x.TimeGroups.Any(y => y == days[0])).ToList();
            List<Time> wt = instance.Times.Where(x => x.TimeGroups.Any(y => y == days[1])).ToList();
            List<Time> sr = instance.Times.Where(x => x.TimeGroups.Any(y => y == days[2])).ToList();
            List<Time> czw = instance.Times.Where(x => x.TimeGroups.Any(y => y == days[3])).ToList();
            List<Time> pt = instance.Times.Where(x => x.TimeGroups.Any(y => y == days[4])).ToList();

            for (int i = 0; i < pon.Count; i++)
            {
                PlanViewModel p = new PlanViewModel();

                p.Godzina = (7 + i).ToString() + ":00";
                List<Event> events = instance.Events.Where(x => (x.Time == pon[i] || (i > 0 && (x.Duration == 2 || x.Duration == 3) && x.Time == pon[i - 1]) || ((i > 1 && x.Duration == 3 && x.Time == pon[i - 2]))) && x.EventResources.Any(y => y.Role?.Contains("Class") == true && y.Resource.Id == classID)).ToList();
                foreach (var item in events)
                {
                    p.Poniedziałek += item.Name + " ";
                }
                events = instance.Events.Where(x => (x.Time == wt[i] || (i > 0 && (x.Duration == 2 || x.Duration == 3) && x.Time == wt[i - 1]) || ((i > 1 && x.Duration == 3 && x.Time == wt[i - 2]))) && x.EventResources.Any(y => y.Role?.Contains("Class") == true && y.Resource.Id == classID)).ToList();
                foreach (var item in events)
                {
                    p.Wtorek += item.Name + " ";
                }
                events = instance.Events.Where(x => (x.Time == sr[i] || (i > 0 && (x.Duration == 2 || x.Duration == 3) && x.Time == sr[i - 1]) || ((i > 1 && x.Duration == 3 && x.Time == sr[i - 2]))) && x.EventResources.Any(y => y.Role?.Contains("Class") == true && y.Resource.Id == classID)).ToList();
                foreach (var item in events)
                {
                    p.Środa += item.Name + " ";
                }
                events = instance.Events.Where(x => (x.Time == czw[i] || (i > 0 && (x.Duration == 2 || x.Duration == 3) && x.Time == czw[i - 1]) || ((i > 1 && x.Duration == 3 && x.Time == czw[i - 2]))) && x.EventResources.Any(y => y.Role?.Contains("Class") == true && y.Resource.Id == classID)).ToList();
                foreach (var item in events)
                {
                    p.Czwartek += item.Name + " ";
                }
                events = instance.Events.Where(x => (x.Time == pt[i] || (i > 0 && (x.Duration == 2 || x.Duration == 3) && x.Time == pt[i - 1]) || ((i > 1 && x.Duration == 3 && x.Time == pt[i - 2]))) && x.EventResources.Any(y => y.Role?.Contains("Class") == true && y.Resource.Id == classID)).ToList();
                foreach (var item in events)
                {
                    p.Piątek += item.Name + " ";
                }
               
                plan.Add(p);
            }

            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    dataGridView1.DataSource = plan;
                });
            }
            else
            {
                dataGridView1.DataSource = plan;
            }
        }

        void RefreshComboBoxData()
        {
            InstancesSource.DataSource = context.Instances.ToList();
            SelectInstanceComboBox.DisplayMember = "IdText";
            SelectInstanceComboBox.ValueMember = "Id";
            SelectInstanceComboBox.DataSource = InstancesSource.DataSource;
        }

        void RefreshClass()
        {
            ClassComboBox.DisplayMember = "Name";
            ClassComboBox.ValueMember = "Id";
            ClassSource.DataSource = context.Instances.Where(x => x.Id == (int)SelectInstanceComboBox.SelectedValue).Include(x => x.Resources
                                               .Select(y => y.Type)).FirstOrDefault()?.Resources.Where(x => x.Type.Name.Contains("Class")).ToList();
            ClassComboBox.DataSource = ClassSource.DataSource;
        }
        private void LoadToDatabaseButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new OpenFileDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && fbd.CheckPathExists)
                {
                    XMLLoader loader = new XMLLoader();
                    loader.LoadToDatabase(fbd.FileName);
                    RefreshComboBoxData();
                }
            }
        }

        private void ResolveButton_Click(object sender, EventArgs e)
        {
            int selectedInstanceId = (int)SelectInstanceComboBox.SelectedValue;
            int selectedClassId = (int)ClassComboBox.SelectedValue;

            int iteration = Int32.Parse(IterationTextBox.Text);
            int tabuDuration = Int32.Parse(TabuDurationTextBox.Text);
            int neighborhoodSize = Int32.Parse(NeighborhoodSizeTextBox.Text);
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                sm.iteration = iteration;
                sm.tabuDuration = tabuDuration;
                sm.neighborhoodSize = neighborhoodSize;
                sm.WriteOnConsol = WriteOnConsol;
                sm.EnableButtons = EnableButtons;
                sm.ResolveSimpleProblem(selectedInstanceId);
                LoadDataView(selectedClassId, sm.bestInstance);
            })).Start();
        }

        public void WriteOnConsol(string text)
        {
            ConsoleRichTextBox.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                ConsoleRichTextBox.AppendText(text);
            });
        }

        public void EnableButtons(bool enable)
        {
            LoadToDatabaseButton.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                LoadToDatabaseButton.Enabled = enable;
            });

            ResolveButton.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                ResolveButton.Enabled = enable;
            });
        }


        private void ConsoleRichTextBox_TextChanged(object sender, EventArgs e)
        {
            ConsoleRichTextBox.SelectionStart = ConsoleRichTextBox.Text.Length;
            ConsoleRichTextBox.ScrollToCaret();
        }

        private void SelectInstanceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshClass();
        }

        private void ClassComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sm.bestInstance != null)
            {
                LoadDataView((int)ClassComboBox.SelectedValue, sm.bestInstance);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sm.EvaluateBestInstanceWithRaport();
        }
      
    }
}
