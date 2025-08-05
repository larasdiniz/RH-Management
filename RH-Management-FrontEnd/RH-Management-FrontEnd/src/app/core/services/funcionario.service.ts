import { HttpClient } from "@angular/common/http";
import { Funcionario } from "../models/funcionario.model";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class FuncionarioService {
  private api = 'http://localhost:5122/api/funcionarios';
  constructor(private http: HttpClient) {}

  getAll() { return this.http.get<Funcionario[]>(this.api); }
  getById(id: number) { return this.http.get<Funcionario>(`${this.api}/${id}`); }
  create(f: Funcionario) { return this.http.post(this.api, f); }
  update(f: Funcionario) { return this.http.put(`${this.api}/${f.id}`, f); }
  delete(id: number) { return this.http.delete(`${this.api}/${id}`); }
  getSalarioMedio() { return this.http.get<number>(`${this.api}/salario-medio`); }
}