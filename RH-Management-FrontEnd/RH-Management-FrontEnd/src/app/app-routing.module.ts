import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './pages/home/home.component';
import { FuncionarioFormComponent } from './pages/funcionario-form/funcionario-form.component';
import { FuncionarioDetalheComponent } from './pages/funcionario-detalhe/funcionario-detalhe.component';
import { RelatorioComponent } from './pages/relatorio/relatorio.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'funcionarios/novo', component: FuncionarioFormComponent },
  { path: 'funcionarios/editar/:id', component: FuncionarioFormComponent },
  { path: 'funcionarios/:id', component: FuncionarioDetalheComponent },
  { path: 'relatorio', component: RelatorioComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
