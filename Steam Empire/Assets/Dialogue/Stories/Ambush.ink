INCLUDE globals.ink

#speaker:Hoodlum
~ambushed = true
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Not_So_Fast
Not so fast. I don't think I've seen you around before. What are you doing here?

* [Just passing by.] -> NOTHING
* [I'm bringing supplies for the foreman.] -> FOOD
* [Get out of my way.] -> GET_LOST


=== NOTHING ===
#audio:Music/emotive_harp
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Nothing
Do you think we're stupid? We've been watching you since you entered the district with that cart of yours. Why are you here? 

* [I have a batch of supplies for the foreman.] -> FOOD
* [Get out of my way.] -> GET_LOST


=== FOOD ===

{NOTHING or GET_LOST: 
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Food
    Thought you could hide that from us, didn't you? We've been starving ever since the rest of the city abandoned us. I suggest you leave the cart and go back to your cozy home. We don't want you here.
    }

{not (NOTHING or GET_LOST): 
    #audio:DialogueVoice/Ambush/hoodlum_dialogue_Not_Nothing_or_Get_Lost
    Really? Supplies? Lucky us. I bet there's food in there. You see, we've been starving ever since the foreman decreased rations. So, since you're working for him... Why don't you help us out? Leave the cart. I promise we'll share. 
    }
* [Maybe we can make a deal?] -> DEAL
* [I can't. I have to deliver everything on that cart.] -> CANT_HELP
* [Get out of my way.] -> GET_LOST


=== GET_LOST ===
{CANT_HELP: 
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Get_Lost_Cant_Help
    I don't see any protection. <>
    }
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Get_Lost
There's three of us, you're alone and not exactly the fighting type...

* [I'm sorry. I wasn't looking for trouble.] -> APOLOGIZE
* [What do you think the foreman is going to do to you when he hears that you stopped me?] -> ARGUE


=== DEAL ===
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Deal
We can deal, alright. Here's a deal: You leave those supplies to us and in return, you get to go home.

* [Wait!  What do you think the foreman is going to do to you when he finds out that you're stealing his supplies?] -> ARGUE


=== CANT_HELP ===
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Cant_Help
This isn't a negotiation. I'm telling you to do something and if you don't then we'll force you.

* {not GET_LOST} [Just get out of my way. I have protection.] -> GET_LOST
* [I have an agreement with the foreman.] -> ARGUE


=== APOLOGIZE ===
#audio:DialogueVoice/Ambush/hoodlum_dialogue_apologize
With his tail between his legs... I thought you'd put up more of a fight. Now hand over your shipment and maybe we'll let you live.
* [Wait! What do you think happens when the foreman finds out that you're stealing from him?] -> ARGUE


=== ARGUE ===
{DEAL: 
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Argue_Deal
    Are you for real? A minute ago you wanted us to pay for his supplies and now you want me to worry about him? Besides... <>
    }
{(GET_LOST or APOLOGIZE): 
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Argue_Get_Lost_Or_Apologize
    So that's who you're working for? Should've guessed. Doesn't matter now. <>
    }
{CANT_HELP: 
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Argue_Cant_Help
    So it wasn't enough for the upper city to abandon us. You have to profit off the disease... But the foreman won't help you now. <>
        }
#audio:DialogueVoice/Ambush/hoodlum_dialogue_Argue
He doesn't care about some outsider. And it's not like he's gonna find out that it was us.
* [\[Continue\]]
    #audio:SFX/Pain/argh4
    #audio:SFX/Pain/flashbang:0.5
    #audio:Music/normal_harp
    -> END



