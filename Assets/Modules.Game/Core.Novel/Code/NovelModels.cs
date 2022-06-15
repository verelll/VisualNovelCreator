using System;
using System.Collections.Generic;
using Architecture.Model;

namespace Game.Novel
{
   public class NovelMainModel : BaseModel
   {
      public NovelScreenModel curScreenModel;
      public NovelDataModel curDataModel;
      public List<NovelDataModel> nextDataModels;
   }
   
   public class NovelScreenModel: BaseModel
   {
      public int source_id;
      public int target_id;
      public object id;
   }

   public class NovelDataModel : BaseModel
   {
      public string description;
      public string choice_description;
      public int id;
      public List<VisualisationProps> visualisations;
      public Card card;

      public event Action<NovelDataModel> OnClickEvent;
      public void OnClick() => OnClickEvent?.Invoke(this);
   }
   
   public class VisualisationProps
   {
      public string title;
      public string description;
      public int id;
   }
   
   public class Card
   {
      public int id;
      public int queststep_id;
      public Game.Novel.Image image;
      public DateTime updated_at;
   }
   
   public class Image
   {
      public string file_id;
   }
}
