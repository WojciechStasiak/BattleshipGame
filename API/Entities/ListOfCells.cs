using System.Collections.Generic;

namespace API.Entities
{
    public class ListOfCells
    {
        public ListOfCells()
        {
        }

        public ListOfCells(List<int> row1, List<int> row2, List<int> row3, List<int> row4, List<int> row5, List<int> row6, List<int> row7, List<int> row8, List<int> row9, List<int> row10)
        {
            this.row1 = row1;
            this.row2 = row2;
            this.row3 = row3;
            this.row4 = row4;
            this.row5 = row5;
            this.row6 = row6;
            this.row7 = row7;
            this.row8 = row8;
            this.row9 = row9;
            this.row10 = row10;
        }

        public List<int> row1 {get;set;}
        public List<int> row2 {get;set;}
        public List<int> row3 {get;set;}
        public List<int> row4 {get;set;}
        public List<int> row5 {get;set;}
        public List<int> row6 {get;set;}
        public List<int> row7 {get;set;}
        public List<int> row8 {get;set;}
        public List<int> row9 {get;set;}
        public List<int> row10 {get;set;}
    }
}