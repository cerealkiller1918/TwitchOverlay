using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib;

namespace Assets
{
     class Test
    {
        public async Task Test1Async()
        {
            TwitchLib.Models.API.v5.Channels.ChannelFollowers f = await TwitchLib.TwitchAPI.Channels.v5.GetChannelFollowersAsync(TwitchInfo.Channel);
        }


    }
}
