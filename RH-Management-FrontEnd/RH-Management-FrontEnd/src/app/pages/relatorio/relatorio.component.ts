import { Component, OnInit } from '@angular/core';
import { Funcionario } from '../../core/models/funcionario.model';
import { FuncionarioService } from '../../core/services/funcionario.service';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';

@Component({
  selector: 'app-relatorio',
  standalone: true,
  imports: [CommonModule, CurrencyPipe, DatePipe],
  template: `<div id="conteudoRelatorio">
<h2>Relatório de Funcionários</h2>
<table>
  <thead><tr><th>Nome</th><th>Cargo</th><th>Admissão</th><th>Salário</th><th>Status</th></tr></thead>
  <tbody>
    <tr *ngFor="let f of funcionarios">
      <td>{{f.nome}}</td>
      <td>{{f.cargo}}</td>
      <td>{{f.dataAdmissao | date}}</td>
      <td>{{f.salario | currency}}</td>
      <td>{{f.status ? 'Ativo' : 'Inativo'}}</td>
    </tr>
  </tbody>
</table>
</div>
<button (click)="gerarPDF()">Gerar PDF</button>`
})
export class RelatorioComponent implements OnInit {
  funcionarios: Funcionario[] = [];

  constructor(private service: FuncionarioService) {}

  ngOnInit() {
    this.service.getAll().subscribe(data => this.funcionarios = data);
  }

  gerarPDF() {
    import('jspdf').then(jsPDF => {
      import('html2canvas').then(html2canvas => {
        const data = document.getElementById('conteudoRelatorio')!;
        html2canvas.default(data).then((canvas: HTMLCanvasElement) => {
          const img = canvas.toDataURL('image/png');
          const pdf = new jsPDF.default();
          pdf.addImage(img, 'PNG', 10, 10, canvas.width * 0.2, canvas.height * 0.2, undefined, 'FAST');
          pdf.save('relatorio-funcionarios.pdf');
        });
      });
    });
  }
}