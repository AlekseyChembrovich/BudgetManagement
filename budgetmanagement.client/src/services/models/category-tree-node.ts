export interface ICategoryTreeNode {
  id: string;
  name: string;
  children: ICategoryTreeNode[] | null;
  deletable: boolean;
}
