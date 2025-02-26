import {sendDelete, sendGet, sendPost, sendPut} from "@/services/basics/http-client.ts";
import type { Result } from "@/services/models/result";
import type { IExpenseRecord } from "@/services/models/expense-record.ts";

export interface ICreateRecordCommand {
  amount: number;
  categoryId: string;
}

export interface IUpdateRecordCommand {
  id: string;
  amount: number;
  categoryId: string;
}

export class RecordService {
  async getList(): Promise<Result<IExpenseRecord[]>> {
    const result: Result<IExpenseRecord[]> = await sendGet<IExpenseRecord[]>(`/records`);

    return result;
  }

  async create(command: ICreateRecordCommand): Promise<Result<IExpenseRecord>> {
    const result: Result<IExpenseRecord> = await sendPost<ICreateRecordCommand, IExpenseRecord>(`/records`, command);

    return result;
  }

  async delete(id: string): Promise<Result<IExpenseRecord>> {
    const result: Result<IExpenseRecord> = await sendDelete<IExpenseRecord>(`/records/${id}`);

    return result;
  }

  async update(command: IUpdateRecordCommand): Promise<Result<IExpenseRecord>> {
    const result: Result<IExpenseRecord> = await sendPut<IUpdateRecordCommand, IExpenseRecord>('/records', command);

    return result;
  }
}
