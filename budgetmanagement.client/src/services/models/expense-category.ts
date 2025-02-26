export interface IExpenseCategory {
  id: string;
  name: string;
  userId: string | null;
  rootId: string | null;
}
