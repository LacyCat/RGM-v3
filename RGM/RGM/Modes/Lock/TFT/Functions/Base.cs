using Exiled.API.Features;
using ProjectMER.Features;
using ProjectMER.Features.Serializable;
using RGM.API.Features;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static RGM.Variables.Variable;

namespace DAONTFT.Core.Functions
{
    public static class Function
    {
        public static MapSchematic LoadMap(string mapName)
        {
            Log.Info($"로드 시도중인 맵: {mapName}");
            MapSchematic map = MapUtils.GetMapData(mapName);

            if (map == null)
            {
                Log.Error($"맵 '{mapName}'을(를) 찾을 수 없습니다. 로드 실패.");
                return null;
            }

            if (!MapUtils.LoadedMaps.ContainsKey(mapName))
                MapUtils.LoadMap(mapName);

            Log.Info($"로드된 맵: {mapName}");

            return map;
        }

        public static List<T> EnumToList<T>()
        {
            Array items = Enum.GetValues(typeof(T));
            List<T> itemList = new List<T>();

            foreach (T item in items)
            {
                List<string> list = new List<string>()
                {
                    "None",
                    "Unknown",
                    "Destroyed"
                };

                if (!list.Contains(item.ToString()))
                    itemList.Add(item);
            }

            return itemList;
        }

        public static AudioClipPlayback PlaySound(Transform transform, string name, float volume = 1, bool loop = false, bool isSpatial = true, float minDistance = 1, float maxDistance = 10)
        {
            AudioPlayer audioPlayer = AudioPlayer.CreateOrGet($"Transform - {transform.position}", onIntialCreation: (p) =>
            {
                p.transform.parent = transform;

                Speaker speaker = p.AddSpeaker("Main", isSpatial: isSpatial, minDistance: minDistance, maxDistance: maxDistance);

                speaker.transform.parent = transform;
                speaker.transform.localPosition = Vector3.zero;
            });

            return audioPlayer.TryPlay(name, volume, loop);
        }

        public static string InsertBreaks(string input, int maxLineLength)
        {
            if (string.IsNullOrEmpty(input) || maxLineLength <= 0)
                return input;

            StringBuilder sb = new StringBuilder();
            int currentLength = 0;
            string[] words = input.Split(' ');

            foreach (string word in words)
            {
                if (currentLength + word.Length > maxLineLength)
                {
                    if (sb.Length > 0)
                        sb.Append('\n');
                    sb.Append(word);
                    currentLength = word.Length;
                }
                else
                {
                    if (currentLength > 0)
                    {
                        sb.Append(' ');
                        currentLength++;
                    }
                    sb.Append(word);
                    currentLength += word.Length;
                }
            }

            return sb.ToString();
        }

        public static bool TryGetLookPlayer(this Player player, float Distance, out Player target, out RaycastHit? raycastHit)
        {
            target = null;
            raycastHit = null;

            if (Physics.Raycast(player.ReferenceHub.PlayerCameraReference.position + player.ReferenceHub.PlayerCameraReference.forward * 0.2f, player.ReferenceHub.PlayerCameraReference.forward, out RaycastHit hit, Distance) &&
                    hit.collider.TryGetComponent<IDestructible>(out IDestructible destructible))
            {
                if (Player.TryGet(hit.collider.GetComponentInParent<ReferenceHub>().gameObject, out Player t) && player != t)
                {
                    target = t;
                    raycastHit = hit;

                    return true;
                }
            }

            return false;
        }

        public static void PlayGlobalAudio(string clipName, float volume = 1, bool loop = false, bool destroyOnEnd = true)
        {
            string notice = $"로드된 오디오: {clipName}";

            Log.Info(notice);

            GlobalPlayer.TryPlay(clipName, volume, loop, destroyOnEnd);
        }
    }
}
