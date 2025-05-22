-- Alterações MovimentacaoEstoque 
ALTER TABLE MovimentacaoEstoque DROP COLUMN IN_ENTRADA;
ALTER TABLE MovimentacaoEstoque ADD TIPO_MOVIMENTACAO INT NOT NULL;

-- Alterações ItemMovimentacaoEstoque 
ALTER TABLE ItemMovimentacaoEstoque ADD CORTESIAS INT NOT NULL DEFAULT(0);
ALTER TABLE ItemMovimentacaoEstoque ADD QUEBRAS INT NOT NULL DEFAULT(0);