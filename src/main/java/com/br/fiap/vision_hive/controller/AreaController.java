package com.br.fiap.vision_hive.controller;



import com.br.fiap.vision_hive.model.Area;
import com.br.fiap.vision_hive.repository.AreaRepository;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cache.annotation.CacheEvict;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.server.ResponseStatusException;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import jakarta.validation.Valid;
import lombok.extern.slf4j.Slf4j;


@RestController
@RequestMapping("areas")
@Slf4j
public class AreaController {

    @Autowired
    private AreaRepository repository;

    @GetMapping
    @Operation(summary = "Listar áreas", description = "Retorna uma lista com todas as áreas")
    @Cacheable("areas")
    public List<Area> index() {
        return repository.findAll();
    }

    @PostMapping
    @CacheEvict(value = "areas", allEntries = true)
    @Operation(responses = @ApiResponse(responseCode = "400", description = "Validação falhou"))
    @ResponseStatus(code = HttpStatus.CREATED)
    public Area create(@RequestBody @Valid Area area) {
        log.info("Cadastrando área " + area.getDescricao());
        return repository.save(area);
    }

    @GetMapping("{id}")
    public ResponseEntity<Area> get(@PathVariable Long id) {
        log.info("Buscando área " + id);
        return ResponseEntity.ok(getArea(id));
    }

    @DeleteMapping("{id}")
    public ResponseEntity<Area> delete(@PathVariable Long id) {
        log.info("Deletando área " + id);
        repository.delete(getArea(id));
        return ResponseEntity.noContent().build();
    }

    @PutMapping("{id}")
    public ResponseEntity<Area> update(@PathVariable Long id, @RequestBody @Valid Area area) {
        log.info("Atualizando área " + id + " com " + area);
        getArea(id);
        area.setId(id);
        repository.save(area);
        return ResponseEntity.ok(area);
    }

    private Area getArea(Long id) {
        return repository.findById(id)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Área não encontrada"));
    }
}
