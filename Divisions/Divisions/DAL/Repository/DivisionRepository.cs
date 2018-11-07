using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Divisions.DAL.Context;
using LocalDbQueriesLibrary;

namespace Divisions.DAL.Repository
{
    class DivisionRepository
    {
        private MyDbContext context = MyDbContext.Context;
               
        internal DivisionRepository()
        {
            
        }

        internal List<Division> DivisionList()
        {
            return context.GetDivisions();
        }
        internal void Add(string name, int parentId)
        {
            context.Add(name, parentId);
        }
        internal void Remove(Division division)
        {
            context.Remove(division);
        }
        internal void Remove(TreeNode division)
        {
            TreeNodeCollection nodes = division.Nodes;

            foreach (TreeNode node in nodes)
            {
                RemoveNodes(node);
                RemoveNode(node);
            }

            RemoveNode(division);

        }
        private void RemoveNodes(TreeNode treeNode)
        {
            foreach (TreeNode tree in treeNode.Nodes)
            {
                RemoveNodes(tree);
                RemoveNode(tree);
            }
        }
        private void RemoveNode(TreeNode node)
        {
            int divisionId = Convert.ToInt32(node.Tag);
            Division division = context.GetDivisions().Find(d => d.Id == divisionId);
            context.Remove(division);
        }

        internal void Edit(Division division)
        {
            context.Edit(division);
        }
    }
}
