using ProjectMER.Features.Objects;
using RGM.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Features;

using static RGM.Variables.ServerManagers;
using RGM.API.DataBases;
using LabApi.Features.Wrappers;
using Exiled.API.Features.Toys;
using ProjectMER.Features;

namespace RGM.Functions
{
    public static class ModeManagers
    {
        public static void PickModes()
        {
            ModeVote.Clear();
            SubModeVote.Clear();

            for (int i = 1; i < 5; i++)
            {
                var StaticModeList = ModeList.Keys.Where(x => ModeList[x].Category == ModeCategory.Public && !ModeVote.ContainsKey(x)).ToList();
                var mode = StaticModeList[UnityEngine.Random.Range(0, StaticModeList.Count())];
                ModeVote.Add(mode, new List<Exiled.API.Features.Player>());

                if (UnityEngine.Random.Range(1, 11) == 1)
                    SubModeVote.Add(Tools.GetRandomValue(ModeList.Keys.Where(x => ModeList[x].Category != ModeCategory.Private && !ModeVote.ContainsKey(x) && ModeList.Keys.Where(x => x.GetModeData().Info != ModeInfo.Set).Contains(x)).ToList()));

                else
                    SubModeVote.Add(ModeType.None);
            }

            List<List<Transform>> Pads = new List<List<Transform>>() { First, Second, Third, Fourth };

            for (int i = 0; i < 4; i++)
            {
                if (Pads[i] == null)
                    continue;

                var modeKeys = ModeVote.Keys.ToList();
                if (i >= modeKeys.Count)
                    continue;

                var modeKey = modeKeys[i];
                if (!ModeList.ContainsKey(modeKey))
                    continue;

                string colorCode = ModeList[modeKey].Color;
                Color padColor;
                if (!string.IsNullOrEmpty(colorCode) && ColorUtility.TryParseHtmlString("#" + colorCode, out Color parsedColor))
                    padColor = parsedColor;
                else
                    padColor = Color.white;

                foreach (var Pad in Pads[i])
                {
                    if (Pad == null)
                        continue;

                    var toy = Pad.GetComponent<PrimitiveObjectToy>();
                    if (toy != null)
                        toy.Color = padColor;
                }
            }

            Color randomColor = Tools.GetRandomColor(true);

            Numbers?.ForEach(x =>
            {
                var toy = x?.GetComponent<PrimitiveObjectToy>();
                if (toy != null)
                    toy.Color = randomColor;
            });
            RandomColors?.ForEach(x =>
            {
                var toy = x?.GetComponent<PrimitiveObjectToy>();
                if (toy != null)
                    toy.Color = randomColor;
            });
            RandomLights?.ForEach(x =>
            {
                var toy = x?.GetComponent<PrimitiveObjectToy>();
                if (toy != null)
                    toy.Color = Tools.GetRandomColor();
            });
            Balls?.ForEach(x =>
            {
                var toy = x?.GetComponent<PrimitiveObjectToy>();
                if (toy != null)
                    toy.Color = Tools.GetRandomColor(true);
            });
        }
    }
}
