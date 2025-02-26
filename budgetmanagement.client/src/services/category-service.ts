import { sendDelete, sendGet, sendPost, sendPut } from "@/services/basics/http-client.ts";
import type { Result } from "@/services/models/result";
import type { IExpenseCategory } from "@/services/models/expense-category";
import type { ICategoryTreeNode } from "@/services/models/category-tree-node";

export interface ICreateCategoryCommand {
  name: string;
  rootId: string | null;
}

export interface IUpdateCategoryCommand {
  id: string;
  name: string;
}

export class CategoryService {
  async getById(id: string): Promise<Result<ICategoryTreeNode>> {
    const result: Result<ICategoryTreeNode> = await sendGet<ICategoryTreeNode>(`/categories/${id}`);

    return result;
  }

  async getList(): Promise<Result<IExpenseCategory[]>> {
    const result: Result<IExpenseCategory[]> = await sendGet<IExpenseCategory[]>(`/categories/list`);

    return result;
  }

  async getTree(): Promise<Result<ICategoryTreeNode[]>> {
    const result: Result<ICategoryTreeNode[]> = await sendGet<ICategoryTreeNode[]>(`/categories/tree`);

    return result;
  }

  async create(command: ICreateCategoryCommand): Promise<Result<IExpenseCategory>> {
    const result: Result<IExpenseCategory> = await sendPost<ICreateCategoryCommand, IExpenseCategory>(`/categories`, command);

    return result;
  }

  async delete(id: string): Promise<Result<IExpenseCategory>> {
    const result: Result<IExpenseCategory> = await sendDelete<IExpenseCategory>(`/categories/${id}`);

    return result;
  }

  async update(command: IUpdateCategoryCommand): Promise<Result<IExpenseCategory>> {
    const result: Result<IExpenseCategory> = await sendPut<IUpdateCategoryCommand, IExpenseCategory>('/categories', command);

    return result;
  }
}
