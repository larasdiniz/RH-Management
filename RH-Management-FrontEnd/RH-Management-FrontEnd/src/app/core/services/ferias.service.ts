import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Ferias } from "../models/ferias.model";

@Injectable({ providedIn: 'root' })
export class FeriasService {
  private api = 'https://localhost:5122/api/ferias';
  constructor(private http: HttpClient) {}

  getByFuncionario(funcionarioId: number) {
    return this.http.get<Ferias[]>(`${this.api}/funcionario/${funcionarioId}`);
  }
  create(f: Ferias) { return this.http.post(this.api, f); }
  update(f: Ferias) { return this.http.put(`${this.api}/${f.id}`, f); }
  delete(id: number) { return this.http.delete(`${this.api}/${id}`); }
}