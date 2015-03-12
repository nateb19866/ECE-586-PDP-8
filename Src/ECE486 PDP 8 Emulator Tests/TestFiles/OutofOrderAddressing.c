/ Program : Add01.pal
/ Date : March 3, 2002
/
/ Desc : This program tests opcode instructions
/		 TAD, AND, ISZ, and JMP with Direct Addressing
/		 in page 1. 2nd DCA should be skipped.
/
/-------------------------------------------
/
/ Code Section
/
*0200			/ start at address 0200
Main, 	cla cll 	/ clear AC and Link
	tad A 		/ add A to Accumulator, AC = 7777
	and A 		/ and A to Accumulator, AC = 7777
	dca 		/ Deposit AC 7777 into C(EA)		
	isz B 		/ increment EA and skip if 0, skip b/c EA = 7777
	dca C 		/ DCA is skipped by ISZ produces EA = 0
	jmp Loop	/ To continue - goto Main
Loop,	hlt 		/ Halt program
	
/
/ Data Section
/
*0300			/ place data at address 0250
A, 	7777 		/ A equals max octal
B, 	7777 		/ B equals 4095 dec
C, 	0

*0230			/ place data at address 0250
Loop2,	dca 		/ Deposit AC 7777 into C(EA)		
	isz B 		/ increment EA and skip if 0, skip b/c EA = 7777
$Main 			/ End of Program; Main is entry point