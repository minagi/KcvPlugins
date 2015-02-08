﻿using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMing.Plugins.Core.Extensions;

namespace AMing.Logger.Helper
{
    public class BattleLogsHelper : LogsHelperBase<Modes.BattleResultList, Modes.BattleResult>
    {
        #region Current

        private static BattleLogsHelper _current = new BattleLogsHelper();

        public static BattleLogsHelper Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion
        protected override int MaxSaveCount { get { return 10; } }
        protected override string FolderName { get { return "Battle"; } }

        protected override Modes.BattleResultList CreateNewList()
        {
            var newdata = base.CreateNewList();
            newdata.AdmiralList = new Modes.SimpleAdmiral[0];

            return newdata;
        }
        protected override void ClearList(Modes.BattleResultList list)
        {
            base.ClearList(list);
        }

        public void Append(KanColleClient kanColleClient, kcsapi_battleresult br, bool isFirstBattle)
        {
            base.Append(list =>
            {
                var newlist = list.List.ToList();
                newlist.Add(new Modes.BattleResult(kanColleClient, br) { IsFirstBattle = isFirstBattle });
                list.List = newlist.ToArray();

                var admiral = new Modes.SimpleAdmiral(kanColleClient);
                if (!list.AdmiralList.Any(x => x.Id == admiral.Id))
                {
                    var newAdmiralList = list.AdmiralList.ToList();
                    newAdmiralList.Add(admiral);
                    list.AdmiralList = newAdmiralList.ToArray();
                }

                return true;
            });
        }

        public void GetInfo(out IList<Modes.BattleResult> list, out IList<Modes.SimpleAdmiral> admiralList, out DateTime lastUpdateDate)
        {
            var allList = base.GetInfo(out list, out lastUpdateDate);

            admiralList = allList.SelectMany(x => x.AdmiralList).Distinct(x => x.Id).ToList();
        }
    }
}