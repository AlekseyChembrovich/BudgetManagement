export interface IExpenseRecord {
  id: string;
  amount: number;
  createdAt: Date;
  categoryId: string;
  userId: string;
  categoryName: string | null;
}
