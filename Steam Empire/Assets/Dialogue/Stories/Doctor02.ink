INCLUDE globals.ink
#speaker:Physician
{infection_reveal: You're back again? <> -> HUB_2}
-> INFECTION_REVEAL_I

=== INFECTION_REVEAL_I ===
#speaker:Physician
~infection_reveal = true
#audio:DialogueVoice/Doctor/doctor_dialogue_Infection_Reveal_I
You're awake. Easy now. I examined you, while you were passed out...
* [\[Continue\]] -> INFECTION_REVEAL_II

=== INFECTION_REVEAL_II ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Infection_Reveal_I_2
You're infected. I'm sorry.
*[What? How... That's impossible.] -> INFECTION_REVEAL_III
    
=== INFECTION_REVEAL_III ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Infection_Reveal_II
From the progression, I'd assume you caught the disease within the last 12 hours. I did what I could to treat it, but... All I can do is slow the spread.
* [...]  -> HUB_2
TODO: This transition might need to be smoother


=== HUB_2 ===
#speaker:Physician
#audio:DialogueVoice/Doctor/doctor_dialogue_Hub_2
Can I do anything else for you?

+ [I have some questions.] -> QUESTIONS_2
+ [Could you look at my infection again?] -> TREATMENT
* [Do you need help finding a cure?]-> OFFER_HELP 
+ [I'll be going.] -> END


=== QUESTIONS_2 ===
What do you want to know?

+ [Are you the only physician around here?] -> DOCTOR_INFO

TODO: Unlocks if talked about doctor and saw the workshop * [WORKSHOP] -> WORKSHOP_INFO

+ [What happened here since the outbreak?] -> FOREMAN_INFO

+ [What do you know about the disease?] -> DISEASE_INFO

+ [Why does the foreman supply you?] -> SUPPLIES_INFO

+ [That is all for now.] -> HUB_2


=== DOCTOR_INFO ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Doctor_Info
That's right. Physicians used to come here once or twice a week. But since the emperor closed the district they won't let them in anymore. I was only allowed in, because I used to have a workshop here. This district isn't as restrictive on research as the other ones. But I had to keep it closed since the outbreak. There are more pressing matters.
* [\[Continue\]] -> QUESTIONS_2
    
    
=== WORKSHOP_INFO ===
-> QUESTIONS_2


=== SUPPLIES_INFO ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Supplies_Info
Why wouldn't he? I'm the only doctor in the entire district. And the foreman doesn't want all his workers to die from the disease. So I take care of his people and he makes sure I have everything I need and can work in peace. Besides, it's in his interest that we find a cure and I'm the only one working on that. 
* [\[Continue\]] -> QUESTIONS_2


=== FOREMAN_INFO ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Forman_Info
We've been on our own since the emperor locked down the district. The magistrate fled and the city watch isn't allowed to enter anymore. So now the foreman is in charge of security around here. He also rations the few supply shipments we get. He's done a fairly good job at keeping the district from rioting. That's all I can tell you.
* [\[Continue\]] -> QUESTIONS_2


=== DISEASE_INFO ===
#audio:DialogueVoice/Doctor/doctor_dialogue_First_Disease_Info
Let's see... It's highly contagious but fairly slow. Infects anything organic.  Wood, plants, animals... Humans. Haven't seen it on stone. That's probably why it hasn't spread to the upper districts. The people here were barely allowed to leave before, let alone now. And... Sorry to say, but I haven't found a cure. It's a death sentence. At least for now.
* [\[Continue\]]  -> QUESTIONS_2

    
=== TREATMENT ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Treatment
I still don't have a cure... Yet. All I can do is slow the spread.
{disease < 50: There isn't anything I can do for you at the moment. Come back later.}
{disease >= 50: Lie down and I'll see what I can do.}
* [\[Continue\]] -> HUB_2


=== OFFER_HELP === 
#audio:DialogueVoice/Doctor/doctor_dialogue_Offer_Help
Not at the moment. I have patients to treat and you are already infected. But come back later, I could use some assistance in my research. 
* [\[Continue\]] -> HUB_2

