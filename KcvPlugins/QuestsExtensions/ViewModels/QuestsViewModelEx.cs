﻿using Grabacr07.KanColleViewer.Properties;
using Grabacr07.KanColleViewer.ViewModels;
using Grabacr07.KanColleViewer.ViewModels.Contents;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using Livet;
using Livet.EventListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMing.QuestsExtensions.ViewModels
{
    public class QuestsViewModelEx : TabItemViewModel// QuestsViewModel
    {

        public override string Name
        {
            get { return Resources.Quests; }
            protected set { throw new NotImplementedException(); }
        }

        #region Current 変更通知プロパティ

        private QuestViewModelEx[] _Current;

        public QuestViewModelEx[] Current
        {
            get { return this._Current; }
            set
            {
                if (this._Current != value)
                {
                    this._Current = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Quests 変更通知プロパティ

        private QuestViewModelEx[] _Quests;

        public QuestViewModelEx[] Quests
        {
            get { return this._Quests; }
            set
            {
                if (this._Quests != value)
                {
                    this._Quests = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region IsUntaken 変更通知プロパティ

        private bool _IsUntaken;

        public bool IsUntaken
        {
            get { return this._IsUntaken; }
            set
            {
                if (this._IsUntaken != value)
                {
                    this._IsUntaken = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region IsEmpty 変更通知プロパティ

        private bool _IsEmpty;

        public bool IsEmpty
        {
            get { return this._IsEmpty; }
            set
            {
                if (this._IsEmpty != value)
                {
                    this._IsEmpty = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        #endregion

        public bool IsInit { get; set; }
        public void Initialize()
        {
            if (IsInit || KanColleClient.Current == null || KanColleClient.Current.Homeport == null || KanColleClient.Current.Homeport.Quests == null)
            {
                return;
            }
            var quests = KanColleClient.Current.Homeport.Quests;

            this.IsUntaken = quests.IsUntaken;
            this.Quests = quests.All.Select(x => new QuestViewModelEx(x)).ToArray();
            this.Current = quests.Current.Select(x => new QuestViewModelEx(x)).ToArray();
            this.IsEmpty = quests.IsEmpty;

            this.CompositeDisposable.Add(new PropertyChangedEventListener(quests)
            {
                { () => quests.IsUntaken, (sender, args) => this.IsUntaken = quests.IsUntaken },
                { () => quests.All, (sender, args) => this.Quests = quests.All.Select(x => new QuestViewModelEx(x)).ToArray() },
                { () => quests.Current, (sender, args) => this.Current = quests.Current.Select(x => new QuestViewModelEx(x)).ToArray() },
                { () => quests.IsEmpty, (sender, args) => this.IsEmpty = quests.IsEmpty }
            });
            IsInit = true;
        }
    }
}
