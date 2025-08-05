import { Injectable } from "@angular/core";
import { HistoricoAlteracao } from "../models/historico.model";
import { HttpClient } from "@angular/common/http";

@Injectable({ providedIn: 'root' })
export class HistoricoService {
  private api = 'https://localhost:5122/api/historico';
  constructor(private http: HttpClient) {}

  getByFuncionario(funcionarioId: number) {
    return this.http.get<HistoricoAlteracao[]>(`${this.api}/funcionario/${funcionarioId}`);
  }
}