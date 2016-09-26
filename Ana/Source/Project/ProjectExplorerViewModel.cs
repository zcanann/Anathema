﻿namespace Ana.Source.Project
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using ProjectItems;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Project Explorer
    /// </summary>
    internal class ProjectExplorerViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(ProjectExplorerViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ProjectExplorerViewModel" /> class
        /// </summary>
        private static Lazy<ProjectExplorerViewModel> projectExplorerViewModelInstance = new Lazy<ProjectExplorerViewModel>(
                () => { return new ProjectExplorerViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        private ReadOnlyCollection<ProjectItemViewModel> projectItems;

        /// <summary>
        /// Prevents a default instance of the <see cref="ProjectExplorerViewModel" /> class from being created
        /// </summary>
        private ProjectExplorerViewModel() : base("Project Explorer")
        {
            this.ContentId = ToolContentId;
            this.AddNewFolderItem = new RelayCommand(() => this.AddNewFolderItemExecute(), () => true);
            this.AddNewAddressItem = new RelayCommand(() => this.AddNewAddressItemExecute(), () => true);
            this.AddNewScriptItem = new RelayCommand(() => this.AddNewScriptItemExecute(), () => true);
            this.IsVisible = true;

            this.projectItems = new ReadOnlyCollection<ProjectItemViewModel>(new List<ProjectItemViewModel>());

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ReadOnlyCollection<ProjectItemViewModel> ProjectItems
        {
            get
            {
                return projectItems;
            }
            set
            {
                projectItems = value;
                RaisePropertyChanged(nameof(this.ProjectItems));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddNewFolderItem { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddNewAddressItem { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddNewScriptItem { get; private set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ProjectExplorerViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ProjectExplorerViewModel GetInstance()
        {
            return projectExplorerViewModelInstance.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddNewFolderItemExecute()
        {
            AddNewProjectItem(new FolderItem());
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddNewAddressItemExecute()
        {
            AddNewProjectItem(new AddressItem());
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddNewScriptItemExecute()
        {
            AddNewProjectItem(new ScriptItem());
        }

        private void AddNewProjectItem(ProjectItem projectItem)
        {
            List<ProjectItemViewModel> NewItems = new List<ProjectItemViewModel>(this.ProjectItems);
            NewItems.Add(new ProjectItemViewModel(projectItem));
            this.ProjectItems = new ReadOnlyCollection<ProjectItemViewModel>(NewItems);
        }
    }
    //// End class
}
//// End namespace