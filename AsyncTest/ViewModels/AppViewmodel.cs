using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace AsyncTest.ViewModels
{
    public class AppViewmodel : ViewModel
    {

        public AppViewmodel()
        {
            ItemList = new ObservableCollection<MyItem>();
            PorgressReporter = new Progress<int>();
            PorgressReporter.ProgressChanged += (object sender, int e) => this.Progress = (uint)e; 

        }

        private async Task Asyncmethod(IProgress<int> progress)
        {

            List<Task<MyItem>> tasks = new List<Task<MyItem>>();


            var results = await Task.WhenAll(tasks);

            int i= 0;
            foreach(var it in results)
            {
                i++;
                this.ItemList.Add(it);
                progress.Report((i * 100) / 5);
            }
                
        }

        private void LoadSync()
        {
            IsLoading = true;
            for(int i=0; i< Quantum; i++)
            {
                this.ItemList.Add(MyItem.FromRandom());
                Progress = (uint)((i + 1) * 100 / Quantum);
            }
            IsLoading = false;
        }


        private async Task LoadAsync(IProgress<int> reporter)
        {
            IsLoading = true;
            for (int i = 0; i < Quantum; i++)
            {
                var res = await Task.Run(() => MyItem.FromRandom());

                this.ItemList.Add(res);
                reporter.Report((int)((i + 1) * 100 / Quantum));
            }
            IsLoading = false;
        }

        private async Task LoadParallel(IProgress<int> reporter)
        {

            var tasks = new List<Task<MyItem>>();
            IsLoading = true;
            for (int i = 0; i < Quantum; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    var res = MyItem.FromRandom();
                    reporter.Report((int)((i + 1) * 100 / Quantum)); // progress Repoprt does NOT work this way !!!
                    return res;
                }));
            }
            

            var results = await Task.WhenAll(tasks);

            foreach (var it in results)
                this.ItemList.Add(it);
            IsLoading = false;
        }

        private async Task<List<MyItem>> LoadParallel2(IProgress<int> reporter)
        {
            var output = new List<MyItem>();

            var off = this.ItemList.Count;

            await Task.Run(() =>
            {
                Parallel.For(0, (int)this.Quantum, (int i) =>
                {
                var res = MyItem.FromRandom();

                output.Add(res);
                reporter.Report((int)((output.Count) * 100 / Quantum));
                });

            });

            return output;
        }


        

        // backgroundworker is kind of legacy ... i do not use it... 
        private void StartCustomBackgroundWorker()
        {

            var progreporter = new Progress<MyItem>();
            progreporter.ProgressChanged += (object sender, MyItem it) => this.ItemList.Add(it); 

            Task.Run(() =>
            {
                var rand = new Random();
                while(this.WorkerActive)
                {
                    Thread.Sleep(500);
                    this.RandomText = rand.Next(0, 100).ToString();

                    // one approach to change list in the background
                    (progreporter as IProgress<MyItem>).Report(new MyItem() { Name = this.RandomText});

                    // throws an exception !! does not work! list is not threadsafe!
                    //this.ItemList.Add(MyItem.FromRandom()); // Oncollectoin changed not threadsafe!!!
                }
            });

        }


        private BackgroundWorker worker;


        private void startWorker()
        {
            worker = new BackgroundWorker();

            // do some shit in background

            worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs e) =>
            {
                var rand = new Random();
                while(true)
                {
                    Thread.Sleep(500);
                    this.RandomText = rand.Next(0, 100).ToString();

                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                        
                }
            });







        }
        

        

        



    /// <summary>
    /// 
    /// </summary>
    /// 
    #region Properties

        public ICommand LoadCommandSync => new RelayCommand(LoadSync);
        public ICommand LoadCommandAsync => new RelayCommand(() => LoadAsync(this.PorgressReporter));
        public ICommand LoadCommandParallelAsync => new RelayCommand( async () => 
        {
            this.IsLoading = true;
            var res = await LoadParallel2(this.PorgressReporter);
            foreach (var it in res)
                this.ItemList.Add(it);
            this.IsLoading = false;
        });
        public ICommand DeleteCommand => new RelayCommand(() => this.ItemList.Clear());



        public ObservableCollection<MyItem> ItemList{get; set;}

        private uint _progress;
        public uint Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private Progress<int> PorgressReporter { get; set; }

        public uint Quantum { get; set; } = 5;

        private bool _isloading = false;
        public bool IsLoading
        {
            get => _isloading;
            set
            {
                _isloading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private String _randText ="";
        public String RandomText
        {
            get => _randText;
            set
            {
                _randText = value;
                OnPropertyChanged(nameof(RandomText));
            }
        }

        private bool _workerActive = false;
        public bool WorkerActive
        {
            get => _workerActive;
            set
            {
                _workerActive = value;
                OnPropertyChanged(nameof(WorkerActive));
                // i konw better - just for testing
                if(value)
                    StartCustomBackgroundWorker();
            }
        }




        #endregion
    }



    public class MyItem: ViewModel
    {


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }

        }


        public static MyItem FromRandom()
        {
            var rand = new Random();

            // deliberately slowing app
            Thread.Sleep(rand.Next(5000));

            return new MyItem()
            {
                Name = $"TimeSt{rand.Next(9)}",
                Value = rand.NextDouble() * 100
            };
        }

    }



}
