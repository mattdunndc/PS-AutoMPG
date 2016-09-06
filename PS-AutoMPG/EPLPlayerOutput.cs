using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_AutoMPG
{
    public class EPLPlayerOutput
    {
        public string id { get; set; }

        public string web_name { get; set; }

        public string first_name { get; set; }

        public string second_name { get; set; }

        public string value_form { get; set; }

        public string value_season { get; set; }

        public string in_dreamteam { get; set; }

        public string dreamteam_count { get; set; }

       public string form { get; set; }

       public string total_points { get; set; }

        public string event_points { get; set; }

        public string points_per_game { get; set; }

        public string ep_this { get; set; }

        public string ep_next { get; set; }

        public string minutes { get; set; }

        public string goals_scored { get; set; }

        public string assists { get; set; }

        public string clean_sheets { get; set; }

        public string goals_conceded { get; set; }

        public string own_goals { get; set; }

        public string penalties_saved { get; set; }

        public string penalties_missed { get; set; }

        public string yellow_cards { get; set; }

        public string red_cards { get; set; }

        public string saves { get; set; }

        public string bonus { get; set; }

        public string bps { get; set; }

        public string influence { get; set; }

        public string creativity { get; set; }

        public string threat { get; set; }

        public string ict_index { get; set; }

        public string ea_index { get; set; }

        public string element_type { get; set; }

        public string team { get; set; }

        public string home { get; set; }  //FALSE OR TRUE
        public string strength_overall { get; set; }  //strength_overall_home or away
        public string strength_position { get; set; } //strength_attack_home or strength_defence_home

        public string team_opp { get; set; }   ///current_event_fixture__opponent
        public string strength_overall_opp { get; set; }  //strength_overall_home or away
        public string strength_position_opp { get; set; } //strength_attack_home or strength_defence_home

    }

}

