using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CustomTreeView
{
    public class AppViewModel: ViewModel
    {

        public AppViewModel()
        {
            Projects = new ObservableCollection<Project>();

            // dummy data ^^

            var proj1 = new Project() { Name = "Apollo" };
            proj1.Phases.Add(new ProjectPhase() { Name = "Alpha" });
            proj1.Phases.Add(new ProjectPhase() { Name = "Beta" });
            proj1.Phases.Add(new ProjectPhase() { Name = "Gamma" });
            Projects.Add(proj1);


            var proj2 = new Project() { Name = "Berta" };
            proj2.Phases.Add(new ProjectPhase() { Name = "Alpha" });
            proj2.Phases.Add(new ProjectPhase() { Name = "Beta" });
            proj2.Phases.Add(new ProjectPhase() { Name = "Gamma" });
            Projects.Add(proj2);

        }



        public ObservableCollection<Project> Projects
        {
            get; set;
        }



    }


    public class Project : ViewModel
    {

        public Project()
        {
            Phases = new ObservableCollection<ProjectPhase>();
        }

        public ObservableCollection<ProjectPhase> Phases { get; set; }


        public string Name { get; set; }


        private bool _isNodeExpanded = false;
        public bool IsNodeExpanded
        {
            get => _isNodeExpanded;
            set
            {
                _isNodeExpanded = value;
                OnPropertyChanged(nameof(IsNodeExpanded));
                Console.WriteLine($"Expanded {value} - {this.Name}");
            }
        }

    }

    public class ProjectPhase: ViewModel
    {

        public string Name { get; set; }

        private bool _isNodeExpanded = false;
        public bool IsNodeExpanded
        {
            get => _isNodeExpanded;
            set
            {
                _isNodeExpanded = value;
                OnPropertyChanged(nameof(IsNodeExpanded));
                Console.WriteLine($"Expanded {value} - {this.Name}");
            }
        }
    }


}
