# O QUE É:

RemanescentePlugin é um plugin desenvolvido para o Navisworks com o foco de indicar o estado de montagem dos objetos encontrados na NWD. O plugin contém 2 ferramentas:

1. Marcação Manual: Altera o status de montagem do objeto

2. Indicar Parcial: Indica que o objeto está parcialmente montado e o tamanho restante para a montagem completa

# COMO INSTALAR:

1. Abrir o Visual Studio no modo administrador

2. Clonar o repositório git para o Visual Studio

3. Alterar o caminho de saída da compilção do projeto `(Propriedade do RemanescentePlugin)>Compilar>Caminho de saída` para a pasta do Plugin no Navisworks `(Caminho do Navisworks)\Plugins>\Criar Pasta chamada Remanescente)`

4. Iniciar o depurador pelo Roamer do Navisworks `(Propriedade do RemanescentePlugin)>Depurar>Iniciar programa externo` passando o arquivo `(Caminho do Navisworks)\Roamer.exe`

5. Compilar a solução `Compilação>Compilar Solução`

6. Copiar o arquivo Maker.xml de `Configfile` para `(Caminho do Navisworks)\Plugins\Remanescente`

7. Copiar o arquivo AddinRibbon.xaml para `(Caminho do Navisworks)\Plugins\Remanescente\(Criar pasta en-US)`

8. Copiar as imagens de `Images` para `(Caminho do Navisworks)\Plugins\Remanescente\(Criar pasta Images)`

# COMO USAR:

## Marcação Manual:

1. Clique no plugin

2. Selecione o estado de montagem pelo RadioButton

3. Selecione todos os item que desejar alterar o estado de montagem

4. Ao querer parar de alterar os estados de montagem, feche a janela do plugin

## Indicar Parcial

1. Selecione um objeto

2. Faça a medição do tamanho não montado do objeto

3. Clique no plugin