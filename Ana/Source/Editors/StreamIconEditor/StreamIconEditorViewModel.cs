﻿namespace Ana.Source.Editors.StreamIconEditor
{
    using Ana.Source.UserSettings;
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Stream Icon Editor.
    /// </summary>
    internal class StreamIconEditorViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(StreamIconEditorViewModel);

        /// <summary>
        /// The path to the stream icons.
        /// </summary>
        private const String StreamIconsPath = @"Content/Overlay/Images/Buffs/";

        /// <summary>
        /// Singleton instance of the <see cref="StreamIconEditorViewModel" /> class.
        /// </summary>
        private static Lazy<StreamIconEditorViewModel> streamIconEditorViewModelInstance = new Lazy<StreamIconEditorViewModel>(
                () => { return new StreamIconEditorViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        private ObservableCollection<StreamIcon> streamIconList;

        /// <summary>
        /// Prevents a default instance of the <see cref="StreamIconEditorViewModel" /> class.
        /// </summary>
        private StreamIconEditorViewModel() : base("Stream Icon Editor")
        {
            this.ContentId = StreamIconEditorViewModel.ToolContentId;
            this.SelectIconCommand = new RelayCommand<StreamIcon>((streamIcon) => this.UpdateStreamIconPath(streamIcon), (streamIcon) => true);
            this.StreamIconListLock = new Object();
            this.StreamIconItemLock = new Object();

            Task.Run(() => this.RebuildStreamIconList());
            Task.Run(() => MainViewModel.GetInstance().RegisterTool(this));
        }

        /// <summary>
        /// Gets the command to select a target process.
        /// </summary>
        public ICommand SelectIconCommand { get; private set; }

        /// <summary>
        /// Gets the stream icon name.
        /// </summary>
        public String StreamIconName { get; private set; }

        /// <summary>
        /// Gets or sets the selection callback.
        /// </summary>
        public Action SelectionCallBack { get; set; }

        /// <summary>
        /// Gets or sets the icon list access lock.
        /// </summary>
        private Object StreamIconListLock { get; set; }

        /// <summary>
        /// Gets or sets the icon item access lock.
        /// </summary>
        private Object StreamIconItemLock { get; set; }

        /// <summary>
        /// Gets the processes running on the machine.
        /// </summary>
        public ObservableCollection<StreamIcon> StreamIconList
        {
            get
            {
                return this.streamIconList;
            }

            set
            {
                this.streamIconList = value;
                this.RaisePropertyChanged(nameof(this.StreamIconList));
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="StreamIconEditorViewModel" /> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static StreamIconEditorViewModel GetInstance()
        {
            return StreamIconEditorViewModel.streamIconEditorViewModelInstance.Value;
        }

        /// <summary>
        /// Lodas all stream icons from disk.
        /// </summary>
        public void RebuildStreamIconList()
        {
            lock (this.StreamIconListLock)
            {
                this.StreamIconList = new ObservableCollection<StreamIcon>();

                Parallel.ForEach(
                    Directory.EnumerateFiles(StreamIconEditorViewModel.StreamIconsPath).Where(file => file.ToLower().EndsWith(".svg")),
                    SettingsViewModel.GetInstance().ParallelSettingsFast,
                    (filePath) =>
                {
                    StreamIcon streamIcon = new StreamIcon(filePath);

                    lock (this.StreamIconItemLock)
                    {
                        App.Current.Dispatcher.Invoke(delegate
                        {
                            this.StreamIconList.Add(streamIcon);
                            this.RaisePropertyChanged(nameof(this.StreamIconList));
                        });
                    }
                });
            }
        }

        /// <summary>
        /// Updates the stream icon path.
        /// </summary>
        /// <param name="text">The stream icon path.</param>
        private void UpdateStreamIconPath(StreamIcon streamIcon)
        {
            if (streamIcon == null)
            {
                return;
            }

            this.StreamIconName = streamIcon.IconName + ".svg";
            this.SelectionCallBack?.Invoke();
        }
    }
    //// End class
}
//// End namespace