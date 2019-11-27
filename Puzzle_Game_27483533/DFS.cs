using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Game_27483533
{
    class DFS
    {
        private Stack<string> open = new Stack<string>();
        //private ArrayList arrOpen = new ArrayList();
        private ArrayList arrState = new ArrayList();
        private ArrayList arrDepth = new ArrayList();

        private Dictionary<string, int> depth = new Dictionary<string, int>();

        private string stateGoal = "123456780";
        private string startState = "";
        private string currState = "";
        private int level = 0;
        private int blank = 0;
        private int counter = 0;

        private Boolean found = false;

        public DFS(string boardState)
        {
            startState = boardState;
            addToOpenStack(startState, "null");
        }

        public void dfsSearch()
        {
            while (open.Count != 0)
            {
                currState = open.Pop();
                if (currState.Equals(stateGoal))
                {
                    found = true;
                    break;
                }
                else
                {
                    if (isTransitionValid("left", currState))
                    {
                        string newState = swap(currState, "left");
                        addToOpenStack(newState, currState);
                        counter++;
                    }
                    if (isTransitionValid("right", currState))
                    {
                        string newState = swap(currState, "right");
                        addToOpenStack(newState, currState);
                        counter++;
                    }
                    if (isTransitionValid("up", currState))
                    {
                        string newState = swap(currState, "up");
                        addToOpenStack(newState, currState);
                        counter++;
                    }
                    if (isTransitionValid("down", currState))
                    {
                        string newState = swap(currState, "down");
                        addToOpenStack(newState, currState);
                        counter++;
                    }
                }
            }
        }

        private void addToOpenStack(string child, string parent)
        {
            int value = 0;

            if (!(depth.ContainsKey(child)))
            {
                if (parent == "null")
                {
                    level = 0;
                }
                else
                {
                    if (depth.TryGetValue(parent, out value))
                    {
                        level = value + 1;
                    }
                }
                depth.Add(child, level);
                open.Push(child);
                arrState.Add(child);
                
            }
        }

        private Boolean isTransitionValid(string inMove, string state)
        {
            blank = state.IndexOf("0");

            if (inMove == "left")
                if ((blank != 0) && (blank != 3) && (blank != 6))
                    return true;

            if (inMove == "right")
                if ((blank != 2) && (blank != 5) && (blank != 8))
                    return true;

            if (inMove == "up")
                if ((blank != 0) && (blank != 1) && (blank != 2))
                    return true;

            if (inMove == "down")
                if ((blank != 6) && (blank != 7) && (blank != 8))
                    return true;

            return false;
        }

        private string swap(string state, string inMove)
        {
            blank = state.IndexOf("0");
            string swappedState = "";

            if (inMove == "left")
                swappedState = state.Substring(0, blank - 1) + "0" + state[blank - 1] + state.Substring(blank + 1);

            if (inMove == "right")
                swappedState = state.Substring(0, blank) + state[blank + 1] + "0" + state.Substring(blank + 2);

            if (inMove == "up")
                if (blank == 8)
                {
                    swappedState = state.Substring(0, blank - 3) + "0" + state.Substring(blank - 3, 3);
                }
                else
                    swappedState = state.Substring(0, blank - 3) + "0" + state.Substring(blank - 2, 2) + state[blank - 3] + state[blank + 1] + state.Substring(blank + 2);

            if (inMove == "down")
                swappedState = state.Substring(0, blank) + state.Substring(blank + 3, 1) + state.Substring(blank + 1, 2) + "0" + state.Substring(blank + 4);//ERROR  vir length van die string is te lank

            return swappedState;
        }

        public Boolean getFound()
        {
            return found;
        }

        public int getCounter()
        {
            return counter;
        }
    
    }
}
