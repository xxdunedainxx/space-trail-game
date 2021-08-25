using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.scripts.core.constants;

namespace Assets.scripts.ui.asset_loaders
{
    static class SpriteLoader
    {
        public static string bookShelfNewBookRemoved = "bookShelfNewBookRemoved";
        public static string spaceCrewAdventureFlyer = ConstStrings.StoryLineStrings.SPACE_CREW_STORY_LINE;
        public static string newBeginningsFlyer = ConstStrings.StoryLineStrings.CORPORATE_STORY_LINE;
        public static string marsAdventureFlyer = ConstStrings.StoryLineStrings.MARS_STORY_LINE;

        static Dictionary<string, string> AssetLookupTable = new Dictionary<string, string>(){
            {SpriteLoader.bookShelfNewBookRemoved ,"Images/chapters/chapter1/Levels/LectureHall/book-shelf-new-book-removed"},
            {SpriteLoader.spaceCrewAdventureFlyer ,"Images/chapters/chapter1/Levels/hallway/Space-crew-adventure-flyer"},
            {SpriteLoader.newBeginningsFlyer ,"Images/chapters/chapter1/Levels/hallway/New-beginnings-corporate-flyer"},
            {SpriteLoader.marsAdventureFlyer ,"Images/chapters/chapter1/Levels/hallway/Mars-adventure-flyer"}
        };

        public static Sprite FetchSprite(string itemToFetch)
        {
            if (SpriteLoader.AssetLookupTable.ContainsKey(itemToFetch))
            {
                return Resources.Load<Sprite>(SpriteLoader.AssetLookupTable[itemToFetch]);
            }
            else
            {
                throw new Exception($"Sprite loader issue, could not find asset {itemToFetch}");
            }
            
        }
    }
}
