using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRPmodifierAPI
{
    public class DRPenv
    {
        public string FILENAMETEXTBOX { get; set;  }
        public string JOINSECRETTEXTBOX { get; set; }
        public string PARTYIDTEXTBOX { get; set; }
        public string SMALLIMAGEKEYTEXTBOX { get; set; }
        public string LARGEIMAGEKEYTEXTBOX { get; set; }
        public string SMALLIMAGETEXTBOX { get; set; }
        public string ENDTIMEBOX { get; set; }
        public string STATETEXTBOX { get; set; }
        public string CLIENTIDTEXTBOX { get; set; }
        public string LARGEIMAGETEXTBOX { get; set; }
        public string DETAILSTEXTBOX { get; set; }

        public DRPenv(string FileName, string JoinSecret, string PartyID, string SmallImageKey, string LargeImageKey, string SmallImageText,
            string EndTime, string StateText, string ClientID, string LargeImageText, string DetailsText)
        {
            FILENAMETEXTBOX = FileName;
            JOINSECRETTEXTBOX = JoinSecret;
            PARTYIDTEXTBOX = PartyID;
            SMALLIMAGEKEYTEXTBOX = SmallImageKey;
            LARGEIMAGEKEYTEXTBOX = LargeImageKey;
            SMALLIMAGETEXTBOX = SmallImageText;
            ENDTIMEBOX = EndTime;
            STATETEXTBOX = StateText;
            CLIENTIDTEXTBOX = ClientID;
            LARGEIMAGETEXTBOX = LargeImageText;
            DETAILSTEXTBOX = DetailsText;
        }

        public string[] ToArray()
        {
            string[] values = new string[11];
            values[0] = FILENAMETEXTBOX;
            values[1] = JOINSECRETTEXTBOX;
            values[2] = PARTYIDTEXTBOX;
            values[3] = SMALLIMAGEKEYTEXTBOX;
            values[4] = LARGEIMAGEKEYTEXTBOX;
            values[5] = SMALLIMAGETEXTBOX;
            values[6] = ENDTIMEBOX;
            values[7] = STATETEXTBOX;
            values[8] = CLIENTIDTEXTBOX;
            values[9] = LARGEIMAGETEXTBOX;
            values[10] = DETAILSTEXTBOX;
            return values;
        }

        public static DRPenv ConvertBack(PostDRPenv env)
        {
            return new DRPenv(env.FILENAMETEXTBOX, env.JOINSECRETTEXTBOX, env.PARTYIDTEXTBOX, env.SMALLIMAGEKEYTEXTBOX, env.LARGEIMAGEKEYTEXTBOX,
                env.SMALLIMAGETEXTBOX, env.ENDTIMEBOX, env.STATETEXTBOX, env.CLIENTIDTEXTBOX, env.LARGEIMAGETEXTBOX, env.DETAILSTEXTBOX);
        }
    }
}
