INCLUDE globals.ink

#speaker:Foreman

{talked_to_foreman: -> VISITED_BEFORE}
-> OPENING

=== OPENING ===
#audio:DialogueVoice/Forman/forman_dialogue_First_Approach
You took your time. You were supposed to arrive hours ago.

* [I'm sorry. The road was blocked. I left the cart at the gate...] -> EXPLANATION

 === EXPLANATION ===
 #audio:DialogueVoice/Forman/forman_dialogue_Explanation
You what?! Where are my supplies? Is the cart still at the gate?

*[They were waiting for me. They knocked me out and took everything.] -> AMBUSHERS

=== AMBUSHERS ===
#audio:DialogueVoice/Forman/forman_dialogue_Ambushers
What?! Who ambushed you? Did you get a good look at them?

* [I don't know. They wore masks.] -> ANGERY

=== ANGERY ===
#audio:DialogueVoice/Forman/forman_dialogue_Angery
Useless! I'll have my men make... inquiries. You can leave.

* [I need your help. They stole my pass. {talked_to_guard: The guard at the gate won't let me leave. |I don't think the guard will let me leave.}] -> HELP_PLS
* [I can't get you your shipment back. But I can get you a discount on the next one.] -> DISCOUNT

=== HELP_PLS ===
#audio:DialogueVoice/Forman/forman_dialogue_Help_Pls1
Interesting... That should raise some flags around here.

* [\[Continue\]] -> HELP_PLS_II

=== HELP_PLS_II ===
#audio:DialogueVoice/Forman/forman_dialogue_Help_Pls2
I can help you get out. But it's going to cost you.

* [Alright. Just help me get out.] -> PREPARATIONS
* [How about a discount on future shipments?] -> DISCOUNT_ALT

=== DISCOUNT ===
#audio:DialogueVoice/Forman/forman_dialogue_Discount1
A discount? How awfully nice of you. You lost my shipment, but I have a discount next time. We're running out of food. We needed those supplies. Now I'll have to cut rations again. The situation was tense enough already.

* [\[Continue\]] -> DISCOUNT_II

=== DISCOUNT_II ===
#audio:DialogueVoice/Forman/forman_dialogue_Discount2
I'll take your offer. But you better put that shipment together quickly. And I'm not paying you for the one you botched. 

* [I'll do what I can. But I need your help getting out. {talked_to_guard: They took my pass and the guard won't let me leave. |I think they stole my pass, too. Can't get out without it.}] -> ARGUMENT

=== DISCOUNT_ALT ===
#audio:DialogueVoice/Forman/forman_dialogue_Discount_Alt1
First, you lose my supplies, now you offer me a discount? We're running out of food here. I'll have to cut rations again. The situation was tense enough already.

* [\[Continue\]] -> DISCOUNT_ALT_II

=== DISCOUNT_ALT_II
#audio:DialogueVoice/Forman/forman_dialogue_Discount_Alt2
I'll take the discount. But that won't cover the price you have to pay to get out of here. 

* [Fine. Just get me out.] -> PREPARATIONS


=== ARGUMENT ===
#audio:DialogueVoice/Forman/forman_dialogue_Argument
Right. Of course. Maybe I should just look for a new... partner. Someone more reliable. Someone who ensures the safety of my investments.

* [How many people are even willing to make these deliveries? You need me.] -> OFFER

=== OFFER ===
#audio:DialogueVoice/Forman/forman_dialogue_Offer
Maybe. It would certainly take time I don't have. But getting you out of the district will take time too. And it's very expensive. You'll owe me.

* [Alright. Just help me get out.] -> PREPARATIONS

=== PREPARATIONS ===
#audio:DialogueVoice/Forman/forman_dialogue_Preparations
Good. I'll make the necessary preparations. While you're stuck here you can start paying off your debt. Do some work for me. 

* [\[Continue\]] -> DOCTOR_TASK


=== DOCTOR_TASK ===
~talked_to_foreman = true
#audio:DialogueVoice/Forman/forman_dialogue_Doctor_Task1
There's a physician. She's set up down by the eastern tunnel. Just follow the purple smoke in the sky. She should be by the house in front of it. You can't miss her. She turned the entire area into a makeshift hospital.

* [\[Continue\]] -> DOCTOR_TASK_II

=== DOCTOR_TASK_II ===
#audio:DialogueVoice/Forman/forman_dialogue_Doctor_Task2
I need you to bring her this satchel of supplies. Don't lose them again or you won't be leaving this district after all. Got it?

* [\[Continue\]] -> DOCTOR_TASK_III

=== DOCTOR_TASK_III
#audio:DialogueVoice/Forman/forman_dialogue_Doctor_Task3
Now get going. I have work to do.

+ [I have questions.] -> QUESTIONS
+ [I'm leaving.] -> END

=== VISITED_BEFORE ===
What do you want?

+ [I have questions.] -> QUESTIONS
+ [Nothing.] -> END

=== QUESTIONS ===
#audio:DialogueVoice/Forman/forman_dialogue_Questions
Make it quick.

+ [I better get going.]-> END