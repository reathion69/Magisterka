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
                p.Poniedziałek = instance.Events.Where(x => x.Time == pon[i] && x.EventResources.Any(y => y.ResourceType?.Name?.Contains("Class") == true && y.Resource.Id == classID)).FirstOrDefault()?.Name;
                p.Wtorek = instance.Events.Where(x => x.Time == wt[i] && x.EventResources.Any(y => y.ResourceType?.Name?.Contains("Class") == true && y.Resource.Id == classID)).FirstOrDefault()?.Name;
                p.Środa = instance.Events.Where(x => x.Time == sr[i] && x.EventResources.Any(y => y.ResourceType?.Name?.Contains("Class") == true && y.Resource.Id == classID)).FirstOrDefault()?.Name;
                p.Czwartek = instance.Events.Where(x => x.Time == czw[i] && x.EventResources.Any(y => y.ResourceType?.Name?.Contains("Class") == true && y.Resource.Id == classID)).FirstOrDefault()?.Name;
                p.Piątek = instance.Events.Where(x => x.Time == pt[i] && x.EventResources.Any(y => y.ResourceType?.Name?.Contains("Class") == true && y.Resource.Id == classID)).FirstOrDefault()?.Name;

                plan.Add(p);
            }          

            dataGridView1.DataSource = plan;
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
                                               .Select(y => y.Type)).FirstOrDefault()?.Resources.Where( x=> x.Type.Name.Contains("Class")).ToList();
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
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {                
                sm.WriteOnConsol = WriteOnConsol;
                sm.EnableButtons = EnableButtons;
                sm.ResolveSimpleProblem(selectedInstanceId);
                LoadDataView(selectedClassId, sm.bestInstance);
            })).Start();           
        }

        public void WriteOnConsol(string text)
        {
            ConsoleRichTextBox.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                ConsoleRichTextBox.AppendText(text);
            });          
        }

        public void EnableButtons(bool enable)
        {
            SaveButton.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                SaveButton.Enabled = enable;
            });

            LoadToDatabaseButton.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                LoadToDatabaseButton.Enabled = enable;
            });

            ResolveButton.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                ResolveButton.Enabled = enable;
            });
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            sm.SaveBestInstance();
            RefreshComboBoxData();
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
            sm.LoadEntityFromDB((int)SelectInstanceComboBox.SelectedValue);
            if (sm.instanceToResolve != null)
            {
                LoadDataView((int)ClassComboBox.SelectedValue, sm.instanceToResolve);
            }
        }
    }
}
