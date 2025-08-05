import { ActivatedRoute } from "@angular/router";
import { FuncionarioService } from "../../core/services/funcionario.service";
import { FeriasService } from "../../core/services/ferias.service";
import { HistoricoService } from "../../core/services/historico.service";
import { Funcionario } from "../../core/models/funcionario.model";
import { HistoricoAlteracao } from "../../core/models/historico.model";
import { Ferias } from "../../core/models/ferias.model";
import { Component, OnInit } from "@angular/core";
import { CommonModule, CurrencyPipe, DatePipe } from "@angular/common";

@Component({
  selector: 'app-funcionario-detalhe',
  standalone: true,
  imports: [CommonModule, CurrencyPipe, DatePipe],
  template: `<h3>Detalhes de {{funcionario?.nome}}</h3>
<ul>
  <li>Cargo: {{funcionario?.cargo}}</li>
  <li>Admissão: {{funcionario?.dataAdmissao | date}}</li>
  <li>Salário: {{funcionario?.salario | currency}}</li>
</ul>
<h4>Histórico de Alterações</h4>
<ul>
  <li *ngFor="let h of historico">{{h.dataAlteracao | date}} - {{h.campoAlterado}}: {{h.valorAntigo}} → {{h.valorNovo}}</li>
</ul>
<h4>Férias</h4>
<ul>
  <li *ngFor="let f of ferias">{{f.dataInicio | date}} até {{f.dataTermino | date}}</li>
</ul>`
})
export class FuncionarioDetalheComponent implements OnInit {
  funcionario!: Funcionario;
  historico: HistoricoAlteracao[] = [];
  ferias: Ferias[] = [];

  constructor(
    private route: ActivatedRoute,
    private funcionarioService: FuncionarioService,
    private feriasService: FeriasService,
    private historicoService: HistoricoService
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.funcionarioService.getById(id).subscribe(f => this.funcionario = f);
    this.feriasService.getByFuncionario(id).subscribe(f => this.ferias = f);
    this.historicoService.getByFuncionario(id).subscribe(h => this.historico = h);
  }
}