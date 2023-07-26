INCLUDE globals.ink

TODO: Add foreman dialogue condition
TODO: rephrase of "district/districts"
TODO: Add that PC is not from around here somewhere
TODO: Use global variable to track where to go
#speaker:Physician
{doctor_approached: -> SECOND_APPROACH}
-> INTRODUCTION

=== INTRODUCTION ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Introduction
Who are you? What do you want?

* [I'm looking for a physician. That you?] -> FIRST_APPROACH


=== FIRST_APPROACH ===
~ doctor_approached = true
#audio:DialogueVoice/Doctor/doctor_dialogue_First_Approach
Yes, that is me. Did you need something? I'm really busy...
* [I have some questions.] -> QUESTIONS_1
* [The foreman sent me. I have supplies.] -> FOREMAN_TASK
    

TODO: Line of people that want to be treated?    
=== QUESTIONS_1 ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Questions_1
I do not have time for this. If you need to be treated get in line. Otherwise, I'll need you to leave. 
* [\[Continue\]] -> END

    
=== SECOND_APPROACH ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Second_Approach
Didn't I tell you to leave? What do you want now?
* [The foreman sent me. I have supplies.] -> FOREMAN_TASK 

=== FOREMAN_TASK ===
TODO: What is the status of medical science here? What is needed to combat the disease?
{QUESTIONS_1: Why didn't you start with that? <>}
#audio:DialogueVoice/Doctor/doctor_dialogue_Forman_Task
Just put them down anywhere. Tell the foreman that it's appreciated. But if he really is serious about fighting this disease I'll need some manpower and even more supplies. We need bandages, strong alcohol... 
* [\[Continue\]] -> PASS_OUT


=== PASS_OUT ===
#audio:DialogueVoice/Doctor/doctor_dialogue_Pass_Out
Are you feeling okay?
* [\[Continue\]]
    #audio:SFX/Pain/flashbang:0.5
    #audio:Music/normal_harp
    -> END






