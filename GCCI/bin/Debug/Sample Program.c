/* Hello World program */

#include<stdio.h>

int main()
{
    printf("Hello World\n");
	int i;
	for ( i=0; i < 100; i++ ) {
	   printf("Line %d\n", i);
	}
	
	printf("Press Any Key to Continue\n");  
	getchar();    
	return 0;

}