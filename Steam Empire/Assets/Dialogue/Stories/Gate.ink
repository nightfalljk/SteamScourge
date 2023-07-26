INCLUDE globals.ink

#speaker:Guard

-> GATE

=== GATE ===
{ambushed && talked_to_guard: I told you to get lost. -> END}
+ {not ambushed} [\[Knock {KNOCKING: again.| at the gate.}\]] -> KNOCKING 
TODO: Rename to something more obvious
+ {ambushed} [\[Knock.\]] -> GUARD
+ [\[Leave.\]] -> END


=== KNOCKING ===
(...)
-> GATE


=== GUARD ===
District pass?
* [Of course. One moment.] -> GUARD_II
    

=== GUARD_II ===
(...)
* [I can't find it. It must have been stolen.] -> GUARD_III

=== GUARD_III ===
~talked_to_guard = true
No pass. No passage. Now get lost. -> END
