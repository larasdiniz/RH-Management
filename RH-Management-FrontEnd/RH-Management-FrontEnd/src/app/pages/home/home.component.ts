  import { Component, OnInit } from "@angular/core";
  import { Funcionario } from "../../core/models/funcionario.model";
  import { FuncionarioService } from "../../core/services/funcionario.service";
  import { CommonModule, CurrencyPipe, DatePipe } from "@angular/common";
  import { FormsModule } from "@angular/forms";
  import { RouterModule } from "@angular/router";

  @Component({
    selector: 'app-home',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterModule, CurrencyPipe, DatePipe],
    template: `<h2>Funcionários</h2>
  <input [(ngModel)]="filtroCargo" (input)="filtrarPorCargo()" placeholder="Filtrar por cargo" />
  <div>Salário médio: {{ salarioMedio | currency }}</div>
  <table>
    <thead>
      <tr><th>Nome</th><th>Cargo</th><th>Admissão</th><th>Status</th><th>Ações</th></tr>
    </thead>
    <tbody>
      <tr *ngFor="let f of funcionarios">
        <td>{{f.nome}}</td>
        <td>{{f.cargo}}</td>
        <td>{{f.dataAdmissao | date}}</td>
        <td>{{f.status ? 'Ativo' : 'Inativo'}}</td>
        <td>
          <a [routerLink]="['/funcionarios', f.id]">Ver</a>
          <a [routerLink]="['/funcionarios/editar', f.id]">Editar</a>
        </td>
      </tr>
    </tbody>
  </table>
  <a routerLink="/funcionarios/novo">Novo Funcionário</a>`
  })
  export class HomeComponent implements OnInit {
    funcionarios: Funcionario[] = [];
    salarioMedio = 0;
    filtroCargo = '';

    constructor(private service: FuncionarioService) {}

    ngOnInit() {
      this.carregarFuncionarios();
      this.service.getSalarioMedio().subscribe(valor => this.salarioMedio = valor);
    }

    carregarFuncionarios() {
      this.service.getAll().subscribe(data => this.funcionarios = data);
    }

    filtrarPorCargo() {
      if (!this.filtroCargo) return this.carregarFuncionarios();
      this.funcionarios = this.funcionarios.filter(f => f.cargo.includes(this.filtroCargo));
    }
  }