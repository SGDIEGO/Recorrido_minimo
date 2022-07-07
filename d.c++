#include <stdio.h>
#include <iostream>
#include <vector>
#include <queue>

void menu();

using namespace std;
#define MAX 10005 
#define Node pair< int , int > 
#define INF 1<<30 

//Estructura para comparar
struct cmp {
    bool operator() ( const Node &a , const Node &b ) {
        return a.second > b.second;
    }
};
vector< Node > ady[ MAX ]; 
int distancia[ MAX ];      
bool visitado[ MAX ];      
priority_queue< Node , vector<Node> , cmp > Q; 
int V;                     
int previo[ MAX ];         

//función de inicialización
void init(){
    for( int i = 0 ; i <= V ; ++i ){
        distancia[ i ] = INF;  
        visitado[ i ] = false; 
        previo[ i ] = -1;     
    }
}

//Paso de relajacion
void relajacion( int actual , int adyacente , int peso ){
    //Si la distancia del origen al vertice actual + peso de su arista es menor a la distancia del origen al vertice adyacente
    if( distancia[ actual ] + peso < distancia[ adyacente ] ){
        distancia[ adyacente ] = distancia[ actual ] + peso;  
        previo[ adyacente ] = actual;                         
        Q.push( Node( adyacente , distancia[ adyacente ] ) ); 
    }
}

//Impresion del camino mas corto desde el vertice inicial y final ingresados
void print( int destino ){
    if( previo[ destino ] != -1 )    
        print( previo[ destino ] ); 
    printf("%d " , destino );       
}

void dijkstra( int inicial ){
    init(); 
    Q.push( Node( inicial , 0 ) ); 
    distancia[ inicial ] = 0;      
    int actual , adyacente , peso;
    while( !Q.empty() ){                   
        actual = Q.top().first;           
        Q.pop();                           

        //Si fue visitado entonces pasa a la siguiente iteracion
        if( visitado[ actual ] )
            continue; 
        
        visitado[ actual ] = true;         

        for( int i = 0 ; i < ady[ actual ].size() ; ++i ){ 
            adyacente = ady[ actual ][ i ].first;   
            peso = ady[ actual ][ i ].second;      
            if( !visitado[ adyacente ] ){       
                relajacion( actual , adyacente , peso ); 
            }
        }
    }

    printf("\n*Impresion de camino mas corto*");//cambié puts por prinft
    printf("\n");
    printf("Ingrese vertice destino: ");
    //printf("\n");
    int destino;
    scanf("%d" , &destino );
    printf("\n");
    printf("El camino más corto es: ") ;  print( destino );
    printf("\n");
}