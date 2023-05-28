﻿using NOTlazyZone.ViewModels;
using NOTlazyZone.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using NOTlazyZone.ViewModel;

namespace NOTlazyZone.ViewModels
{
    public  class MainWindowViewModel : BaseViewModel
    {
        #region CommandsMenu
        public ICommand TowarCommand
        {
            get
            {
                return new BaseCommand(CreateMessage);
            }
        }
        //public ICommand TowaryCommand
        //{
        //    get
        //    {
        //        return new BaseCommand(ShowAllTowar);
        //    }
        //}
        #endregion
        //
        #region Commands
        private ReadOnlyCollection<CommandViewModel> _Commands;
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get 
            { 
                if(_Commands == null)
                {
                    List<CommandViewModel> cmds = CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands; 
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel("Messages",new BaseCommand(CreateMessage)),
                new CommandViewModel("Calendar",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Statistics",new BaseCommand(ShowStatistic)),
                new CommandViewModel("Zaplanowane spotkania",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Scheduled meetings",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Exercise",new BaseCommand(ShowExercies)),
                new CommandViewModel("Training Plan",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Diet",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Calories Calculator",new BaseCommand(ShowCaloriesCalculator)),
                new CommandViewModel("List Contact",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Shopping",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Discounts",new BaseCommand(ShowCalendar)),
                new CommandViewModel("Settings",new BaseCommand(ShowSettings)),

            };
        }
        #endregion
        #region Workspaces
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get 
            { 
                if(_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces; 
            } 
        }
        #endregion


        #region Zakładki
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }
        #endregion

        #region Funkcje wywolu.....
        private void CreateMessage()
        {
            MessagesViewModel workspace=new MessagesViewModel();
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }
        private void ShowStatistic()
        {
            StatisticViewModel workspace = Workspaces.FirstOrDefault(vm => vm is StatisticViewModel) as StatisticViewModel;
            if (workspace == null)
            {
                workspace = new StatisticViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }
        private void ShowCalendar()
        {
            TrainingPlanViewModel workspace = Workspaces.FirstOrDefault(vm => vm is TrainingPlanViewModel) as TrainingPlanViewModel;
            if (workspace == null)
            {
                workspace = new TrainingPlanViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void ShowExercies()
        {
            ExerciseViewModel workspace = Workspaces.FirstOrDefault(vm => vm is ExerciseViewModel) as ExerciseViewModel;
            if (workspace == null)
            {
                workspace = new ExerciseViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void ShowCaloriesCalculator()
        {
            CaloriesCalculatorViewModel workspace = Workspaces.FirstOrDefault(vm => vm is CaloriesCalculatorViewModel) as CaloriesCalculatorViewModel;
            if (workspace == null)
            {
                workspace = new CaloriesCalculatorViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void ShowSettings()
        {
            SettingViewModel workspace = Workspaces.FirstOrDefault(vm => vm is SettingViewModel) as SettingViewModel;
            if (workspace == null)
            {
                workspace = new SettingViewModel();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion




    }
}
