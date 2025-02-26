import { sendGet } from "@/services/basics/http-client.ts";
import type { Result } from "@/services/models/result";
import type { IReport } from "@/services/models/report.ts";

export interface IGetReport {
  categoryId: string | null;
  from: Date | null;
  to: Date | null;
}

export class RecordService {
  async get(request: IGetReport): Promise<Result<IReport[]>> {
    let query: string = '/report';
    if (request.categoryId != null) {
      query += `?CategoryId=${request.categoryId}`;
    }
    if (request.from != null && request.to != null) {
      console.log(request.from)
      console.log(request.to)
      let params = `From=${request.from}&To=${request.to}`;
      if (query === '/report') {
        query += `?${params}`;
      }
      else {
        query += `&${params}`;
      }

    }

    const result: Result<IReport[]> = await sendGet<IReport[]>(query);

    return result;
  }
}
