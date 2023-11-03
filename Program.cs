using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

// opponent calculation based on team numbers and week numbers starting at 1


int maxTeams = 12;          // if its not even then give a bye if opponent > maxTeams
int numberOfRounds = 2;     // how many rounds in the competition

//
// write top lines of table
//

Console.Write("   TEAM: ");
for (int team = 1; team <= maxTeams; team++)
{
    Console.Write(team.ToString("D2")+ " | ");
}
Console.WriteLine();
for (int i = 1; i < (maxTeams * 5) + 9; i++)
{
    Console.Write('-');
}
Console.WriteLine();

//
//  generate a table for 'maxTeams' running for 'numberOfRounds'
//

for (int week = 1; week <= (maxTeams - 1) * numberOfRounds; week ++)
{
    Console.Write("Week " + week.ToString("D2") + ": ");
    for (int team = 1; team <= maxTeams; team++)
    {
        int opponent = getOpponent(team, week, maxTeams);
        Console.Write(opponent > maxTeams ? "-- | " : opponent.ToString("D2") + " | ");
    }
    Console.WriteLine();
}


//
// Given a team, weeknumber and number of teams in the competition
// returns the opposing team for that week 
//
static int getOpponent(int team, int week, int maxTeams)
{
    if (maxTeams % 2 != 0)    // make sure we have an even numbers of teams,  matches against extra team get a bye *deal with this outside of here*
    {
        maxTeams++;
    }

    int thisWeek = week % (maxTeams - 1);  // calculate a week number that fits within the round
    
    if (thisWeek == 0)
    {
           // last week of a round
        return (maxTeams - (team - 1)); 
    }
    else if (thisWeek % 2 != 0)
    {
        // odd week
        //
        //    (pos) weekstart+2 = maxteams-1      (pos) (weekstart + 1)/2 + (maxteams/2) = maxteams        last (weekstart+1)/2  + maxteams/2
        //
        int firstMatchPoint = thisWeek + 2;
        int secondMatchPoint = (thisWeek + 1) / 2 + (maxTeams / 2);
        if (team == maxTeams)
        {
            return secondMatchPoint;
        }
        else if (team == secondMatchPoint)
        {
            return maxTeams;
        }
        else if (team == firstMatchPoint)
        {
            return maxTeams - 1;
        }
        else
        {
            return ((thisWeek + (maxTeams - (team - 1))) % maxTeams + 1) - (team > firstMatchPoint ? 1 : 0);
        }
    }
    else
    {
        // even week
        //
        //     (pos)  (weekstart/2 + 1) = maxteams  (pos) 2*(weekstart/2 + 1)    last> (weekstart/2) + 1
        //
        int firstMatchPoint = (thisWeek / 2) + 1;
        int secondMatchPoint = firstMatchPoint * 2;
        if (team == maxTeams)
        {
            return firstMatchPoint;
        }
        else if (team == firstMatchPoint)
        {
            return maxTeams;
        }
        else
        {
            return ((thisWeek + (maxTeams - (team - 1))) % maxTeams + 1) - (team >= secondMatchPoint ? 1 : 0);
        }
    }
}
