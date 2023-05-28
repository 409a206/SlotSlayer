using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Map{
    public class MapManager : MonoBehaviour
    {
        public MapConfig config;
        public MapView view;
        public Map CurrentMap{get;private set;}
        // Start is called before the first frame update
        void Start()
        {
            if(PlayerPrefs.HasKey("Map")) {
                var mapJson = PlayerPrefs.GetString("Map");
                var map = JsonConvert.DeserializeObject<Map>(mapJson);
                //using this instead of .Contains()
                if(map.path.Any(p => p.Equals(map.GetBossNode().point))) {
                    // player has already reached the boss, generate a new map
                    GenerateNewMap();
                } else {
                    CurrentMap = map;
                    //player has not reached the boss yet, load the current map
                    view.ShowMap(map);
                }
            } else {
                GenerateNewMap();
            }
        }

        private void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            Debug.Log(map.ToJson());
            view.ShowMap(map);
        }

        public void SaveMap() {
            if(CurrentMap == null) return;

            var json = JsonConvert.SerializeObject(CurrentMap, Formatting.Indented,
                new JsonSerializerSettings  {ReferenceLoopHandling = ReferenceLoopHandling.Ignore} );
                PlayerPrefs.SetString("Map", json);
                PlayerPrefs.Save();
        }
        
        private void OnApplicationQuit() {
            SaveMap();
        }
    }

}
